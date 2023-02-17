using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace HogwartsPotions.Services
{
    public class PotionService : IPotionService
    {
        private readonly HogwartsContext _context;

        public PotionService(HogwartsContext context)
        {
            _context = context;
        }

        public async Task<List<Potion>> GetAllPotion()
        {
            var potions = await _context.Potions
                .Include(p => p.Student).ThenInclude(s => s.Room).ThenInclude(r => r.Residents)
                .Include(p => p.Recipe).ThenInclude(r => r.Ingredients)
                .Include(p => p.Ingredients).ToListAsync();
            return potions;
        }

        public async Task<Potion> AddPotion(Potion potion)
        {
            var student = await _context.Students.
                    FirstOrDefaultAsync(s => s.Name == potion.Student.Name);

            if (student == null)
            {
                student = new Student
                {
                    Name = potion.Student.Name,
                    PetType = potion.Student.PetType,
                    HouseType = potion.Student.HouseType,
                    Room = potion.Student.Room,
                };
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
            }

            potion.Student = student;
            var studentName = potion.Student.Name;
            var numberOfDiscoveries = await NumberOfDiscoveriesByStudent((int)potion.Student.ID);

            //ingredients < 5
            if (potion.Ingredients.Count < 5)
            {
                potion.BrewingStatus = BrewingStatus.Brew;
                potion.Name = $"{studentName} brew";
                await _context.Potions.AddAsync(potion);
                await _context.SaveChangesAsync();
                return potion;

            }
            // ingredients = 5
            if (potion.Ingredients.Count == 5)
            {
                var allRecipes = await _context.Recipes.Include(r => r.Ingredients).Include(r => r.Student)
                    .ToListAsync();
                /*foreach (Recipe recipe in allRecipes)
                {
                    if (potion.Ingredients.OrderBy(i => i.Name).SequenceEqual(recipe.Ingredients.OrderBy(i => i.Name)))
                    {
                        potion.BrewingStatus = BrewingStatus.Replica;
                        break;
                    }
                    else
                    {
                        potion.BrewingStatus = BrewingStatus.Discovery;
                    }

                }*/
                //only checks the ingredient name, not the whole ingredient object
                foreach (Recipe recipe in allRecipes)
                {
                    var potionIngredientNames = potion.Ingredients.Select(i => i.Name).OrderBy(name => name);
                    var recipeIngredientNames = recipe.Ingredients.Select(i => i.Name).OrderBy(name => name);

                    if (potionIngredientNames.SequenceEqual(recipeIngredientNames))
                    {
                        potion.BrewingStatus = BrewingStatus.Replica;
                        break;
                    }
                    else
                    {
                        potion.BrewingStatus = BrewingStatus.Discovery;
                    }
                }

            }

            if (potion.BrewingStatus == BrewingStatus.Discovery)
            {
              
                potion.Name = $"{studentName}'s discovery #{numberOfDiscoveries}";
                //save recipe if brew discovery
                potion.Recipe = new Recipe
                {
                    Ingredients = potion.Ingredients,
                    Student = potion.Student,
                    Name = $"Recipe for: {potion.Name}"
                };
                await _context.Potions.AddAsync(potion);
                await _context.Recipes.AddAsync(potion.Recipe);
                await _context.SaveChangesAsync();
                return potion;
            }
            if (potion.BrewingStatus == BrewingStatus.Replica)
            {
                potion.Name = $"{studentName}'s replica";
                await _context.Potions.AddAsync(potion);
                await _context.SaveChangesAsync();
                return potion;

            }

            return null;

        }

        public async Task<Potion> GetPotionById(long potionId)
        {
            return await _context.Potions.FindAsync(potionId);
        }

        public async Task<List<Potion>> ExploredPotionsByStudent(int studentId)
        {
            var potionsByStudents = await _context.Potions
                .Include(p => p.Ingredients)
                .Where(p => p.Student.ID == studentId).ToListAsync();
            return potionsByStudents;
        }

        public async Task<int> NumberOfDiscoveriesByStudent(int studentId)
        {
            var discoveriesByStudent = await _context.Potions
                .Where(p => p.Student.ID == studentId && p.BrewingStatus == BrewingStatus.Discovery)
                .CountAsync();

            return discoveriesByStudent + 1;
        }

        public async Task<Potion> ExploratoryBrew(Potion exploratoryPotion)
        {
            var student = await _context.Students.FindAsync(exploratoryPotion.Student.ID);
            var brewingStatus = BrewingStatus.Brew;
            //var ingredients = new List<Ingredient>();
            var name = $"{student.Name}'s exploratory brew";
            var newExploratoryPotion = new Potion
            {
                Name = name,
                BrewingStatus = brewingStatus,
                Ingredients = exploratoryPotion.Ingredients,
                Student = student

            };
            _context.Potions.Add(newExploratoryPotion);
            await _context.SaveChangesAsync();
            return newExploratoryPotion;

        }

        //problem that it adds duplicates, ingredients with same name,
        //only need to save the ingredient if it is new
        public async Task<Potion> AddIngredientToPotion(int id, Ingredient ingredient)
        {
            var potion = await _context.Potions.Include(p => p.Ingredients)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.ID == id);
            var potionsIngredients = potion.Ingredients;
            potionsIngredients.Add(ingredient);
            //check replica or discovery
            if (potionsIngredients.Count == 5)
            {
                foreach (Recipe recipe in _context.Recipes)
                {
                    if (potionsIngredients.OrderBy(i => i.Name) == recipe.Ingredients.OrderBy(i => i.Name))
                    {
                        potion.BrewingStatus = BrewingStatus.Replica;
                    }
                    else
                    {
                        potion.BrewingStatus = BrewingStatus.Discovery;
                        var numberOfDiscoveries = await NumberOfDiscoveriesByStudent((int)potion.Student.ID);
                        potion.Name = $"{potion.Student.Name}'s discovery #{numberOfDiscoveries}";
                        potion.Recipe = new Recipe
                        {
                            Ingredients = potion.Ingredients,
                            Student = potion.Student,
                            Name = $"Recipe for: {potion.Name}"
                        };

                    }

                }

                //to update the potion status and name
                _context.Potions.Update(potion);
                await _context.Recipes.AddAsync(potion.Recipe);
                await _context.SaveChangesAsync();
                return potion;
            }

            return potion;
        }

        public async Task<List<Recipe>> HelpBrew(long potionId)
        {
            var recipesWithIngredient = new List<Recipe>();
            var recipes = _context.Recipes.Include(r => r.Ingredients);
            var currentPotion = await _context.Potions
                .Include(p => p.Ingredients)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.ID == potionId);
            if (currentPotion.Ingredients.Count < 5)
            {
                foreach (var potionIngredient in currentPotion.Ingredients)
                {
                    foreach (var recipe in recipes)
                    {
                        foreach (var recipeIngredient in recipe.Ingredients)
                        {
                            if (potionIngredient.Name == recipeIngredient.Name)
                            {
                                recipesWithIngredient.Add(recipe);
                            }
                        }
                    }


                }
            }

            await _context.SaveChangesAsync();
            return recipesWithIngredient;
        }
    }


}

