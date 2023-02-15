using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace HogwartsPotions.Services.Interfaces
{
    public interface IRoomService
    {
        Task AddRoom(Room room);

        Task<Room> GetRoom(long roomId);

        Task<List<Room>> GetAllRooms();

        Task UpdateRoom(Room room);

        Task DeleteRoom(long id);

        Task<List<Room>> GetRoomsForRatOwners();
    }
}
