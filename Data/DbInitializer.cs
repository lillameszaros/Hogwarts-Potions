using System;
using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Data
{
    public class DbInitializer
    {
        public static void Initialize(HogwartsContext context)
        {
            context.Database.EnsureCreated();


            // Look for any students.
            if (context.Students.Any() || context.Rooms.Any())
            {
                return; // DB has been seeded
            }

            Student student1 = new Student
            {
                Name = "Harry Potter",
                HouseType = HouseType.Gryffindor,
                PetType = PetType.Owl
            };

            Student student2 = new Student
            {
                Name = "Hermione Granger",
                HouseType = HouseType.Gryffindor,
                PetType = PetType.Cat
            };

            Student student3 = new Student
            {
                Name = "Luna Lovegood",
                HouseType = HouseType.Ravenclaw,
                PetType = PetType.None
            };

            Student student4 = new Student
            {
                Name = "Draco Malfoy",
                HouseType = HouseType.Slytherin,
                PetType = PetType.None
            };
            Student student5 = new Student
            {
                Name = "Ginny Weasley",
                HouseType = HouseType.Gryffindor,
                PetType = PetType.Owl
            };
            Student student6 = new Student
            {
                Name = "Cho Chang",
                HouseType = HouseType.Ravenclaw,
                PetType = PetType.Rat
            };

            context.Students.AddRange(
                student1,
                student2,
                student3,
                student4,
                student5, 
                student6
            );
            context.SaveChanges();


            Room gryffindorCommonRoom = new Room
            {
                Capacity = 20,
                Residents= new HashSet<Student> { student1, student2, student5 }
            };

            Room ravenclawCommonRoom = new Room
            {
                Capacity = 15,
                Residents = new HashSet<Student> { student3, student6 }
            };

            Room roomOfRequirement = new Room
            {
                Capacity = 100,
                Residents = new HashSet<Student> { student4 }
            };
            context.Rooms.AddRange(
                gryffindorCommonRoom,
                ravenclawCommonRoom,
                roomOfRequirement);

            context.SaveChanges();

            if (context.Ingredients.Any())
            {
                return; // DB has been seeded
            }

            Ingredient catHair = new Ingredient { Name = "Cat hair" };
            Ingredient unicornHorn = new Ingredient { Name = "Unicorn horn" };
            Ingredient cactus = new Ingredient { Name = "Cactus liquid" };
            Ingredient seaweed = new Ingredient { Name = "Seaweed" };
            Ingredient dust = new Ingredient { Name = "Dust" };
            Ingredient nails = new Ingredient { Name = "Nails" };
            Ingredient ratTail = new Ingredient { Name = "Rat tail" };
            Ingredient powder = new Ingredient { Name = "Powder" };
            Ingredient pigEar = new Ingredient { Name = "Pig Ear" };
            Ingredient salt = new Ingredient { Name = "Salt" };
            Ingredient root = new Ingredient { Name = "Root" };
            Ingredient leaf = new Ingredient { Name = "Leaf" };

            context.Ingredients.AddRange(
                catHair,
                unicornHorn,
                seaweed,
                dust,
                cactus,
                nails, 
                ratTail,
                powder,
                pigEar,
                salt,
                root,
                leaf
                );
            context.SaveChanges();

            if (context.Recipes.Any())
            {
                return; // DB has been seeded
            }

            Recipe strengthPotionRecipe = new Recipe
            {
                Name = "Recipe 1",
                Ingredients = new HashSet<Ingredient> { unicornHorn, cactus, ratTail, seaweed, powder },
                Student = student4
            };

            Recipe polyjuicePotionRecipe = new Recipe
            {
                Name = "Recipe 2",
                Ingredients = new HashSet<Ingredient> { catHair, dust, unicornHorn, pigEar, salt },
                Student = student2
            };

            Recipe felixFelicisRecipe
                = new Recipe
            {
                Name = "Recipe 3",
                Ingredients = new HashSet<Ingredient> { nails, cactus, seaweed, root, leaf },
                Student = student1
            };
            context.Recipes.AddRange(
                strengthPotionRecipe,
                polyjuicePotionRecipe,
                felixFelicisRecipe
                );
            context.SaveChanges();

            if (context.Potions.Any())
            {
                return; // DB has been seeded
            }

            Potion lunaPotion1 = new Potion
            {
                Name = "Luna's potion",
                Recipe = strengthPotionRecipe,
                Student = student3,
                Ingredients = new HashSet<Ingredient> { catHair, unicornHorn },
                BrewingStatus = BrewingStatus.Brew
            };
            Potion hermionePotion1 = new Potion
            {
                Name = "Hermione's potion",
                Recipe = polyjuicePotionRecipe,
                Student = student2,
                Ingredients = new HashSet<Ingredient> { catHair, dust, unicornHorn, pigEar, salt },
                BrewingStatus = BrewingStatus.Replica
            };
            Potion dracoPotion1 = new Potion
            {
                Name = "Draco's Potion",
                Recipe = felixFelicisRecipe,
                Student = student4,
                Ingredients = new HashSet<Ingredient> { catHair, dust, cactus, seaweed, dust },
                BrewingStatus = BrewingStatus.Brew
            };
            context.Potions.AddRange(
                lunaPotion1,
                hermionePotion1,
                dracoPotion1);
            context.SaveChanges();

        }
    }

}
