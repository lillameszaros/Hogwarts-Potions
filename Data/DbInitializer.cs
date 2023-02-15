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

            context.Students.AddRange(
                student1,
                student2,
                student3,
                student4
            );
            context.SaveChanges();


            Room gryffindorCommonRoom = new Room
            {
                Capacity = 20,
                Residents= new HashSet<Student> { student1, student2 }
            };

            Room ravenclawCommonRoom = new Room
            {
                Capacity = 15,
                Residents = new HashSet<Student> { student3 }
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

            context.Ingredients.AddRange(
                catHair,
                unicornHorn,
                seaweed,
                dust,
                cactus,
                nails
                );
            context.SaveChanges();

            if (context.Recipes.Any())
            {
                return; // DB has been seeded
            }

            Recipe recipe1 = new Recipe
            {
                Name = "Recipe 1",
                Ingredients = new List<Ingredient> { unicornHorn, cactus },
                Student = student4
            };

            Recipe recipe2 = new Recipe
            {
                Name = "Recipe 2",
                Ingredients = new List<Ingredient> { catHair, dust, unicornHorn },
                Student = student2
            };

            Recipe recipe3
                = new Recipe
            {
                Name = "Recipe 3",
                Ingredients = new List<Ingredient> { seaweed, nails },
                Student = student1
            };
            context.Recipes.AddRange(
                recipe1,
                recipe2,
                recipe3);
            context.SaveChanges();

            if (context.Potions.Any())
            {
                return; // DB has been seeded
            }

            Potion strengthPotion = new Potion
            {
                Name = "Strength Potion",
                Recipe = recipe1,
                Student = student3,
                Ingredients = new List<Ingredient> { catHair, unicornHorn }
            };
            Potion polyjuice = new Potion
            {
                Name = "Polyjuice Potion",
                Recipe = recipe2,
                Student = student2,
                Ingredients = new List<Ingredient> { catHair, dust }
            };
            Potion felix = new Potion
            {
                Name = "Felix Felicis",
                Recipe = recipe2,
                Student = student4,
                Ingredients = new List<Ingredient> { catHair, dust, cactus, seaweed, dust }
            };
            context.Potions.AddRange(
                strengthPotion,
                felix,
                polyjuice);
            context.SaveChanges();

        }
    }

}
