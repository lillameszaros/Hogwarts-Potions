using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Services.Interfaces
{
    public interface IPotionService
    {
        Task<List<Potion>> GetAllPotion();

        Task <Potion>AddPotion(Potion potion);

        Task<Potion> GetPotionById(long potionId);

        Task<List<Potion>> ExploredPotionsByStudent(int studentId);

        Task <Potion>ExploratoryBrew(Potion potion);

        Task<Potion> AddIngredientToPotion(int id, Ingredient ingredient);

        Task<List<Recipe>> HelpBrew(long potionId);

        /*Task<Room> GetRoom(long roomId);


        Task UpdateRoom(Room room);

        Task DeleteRoom(long id);

        Task<List<Room>> GetRoomsForRatOwners();*/
    }
}
