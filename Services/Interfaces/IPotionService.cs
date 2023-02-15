using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Services.Interfaces
{
    public interface IPotionService
    {
        Task<List<Potion>> GetAllPotion();

        Task AddPotion(Potion potion);

        Task<List<Potion>> ExploredPotionsByStudent(int studentId);

        /*Task<Room> GetRoom(long roomId);


        Task UpdateRoom(Room room);

        Task DeleteRoom(long id);

        Task<List<Room>> GetRoomsForRatOwners();*/
    }
}
