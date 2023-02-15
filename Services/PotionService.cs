using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                .Include(p => p.Student).ThenInclude(s=>s.Room).ThenInclude(r=>r.Residents)
                .Include(p => p.Recipe).ThenInclude(r=>r.Ingredients)
                .Include(p => p.Ingredients).ToListAsync();
            return potions;
        }

        public async Task AddPotion(Potion potion)
        {
            if (potion.Ingredients.Count < 5)
            {
                potion.BrewingStatus = BrewingStatus.Brew;
            }
            foreach (Recipe recipe in _context.Recipes)
            {
                if(potion.Ingredients.OrderBy(i => i.Name) == recipe.Ingredients.OrderBy(i => i.Name))
                {
                    potion.BrewingStatus = BrewingStatus.Replica;
                }
                else
                {
                    potion.BrewingStatus = BrewingStatus.Discovery;
                }
                
            }

            if (potion.BrewingStatus == BrewingStatus.Discovery)
            {
                var numberOfDiscoveries = NumberOfDiscoveriesByStudent((int)potion.Student.ID);
                potion.Name = $"{potion.Student.Name}'s discovery #{numberOfDiscoveries}";
                await _context.Potions.AddAsync(potion);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<Potion>> ExploredPotionsByStudent(int studentId)
        {
            var potionsByStudents = await _context.Potions
                .Include(p=>p.Ingredients)
                .Where(p => p.Student.ID == studentId).ToListAsync();
            return potionsByStudents;
        }

        public async Task<int> NumberOfDiscoveriesByStudent(int studentId)
        {
            var discoveriesByStudent = await _context.Potions
                .Where(p => p.Student.ID == studentId && p.BrewingStatus == BrewingStatus.Discovery)
                .CountAsync();

            return discoveriesByStudent+1;
        }

    }
}
