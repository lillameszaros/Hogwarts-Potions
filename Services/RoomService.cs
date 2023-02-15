using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services
{
    public class RoomService : IRoomService
    {
        private readonly HogwartsContext _context;

        public RoomService(HogwartsContext context)
        {
            _context=context;
        }
        public async Task AddRoom(Room room)
        {
            await _context.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoom(long roomId)
        {
            var room = await _context.Rooms
                .Include(r=>r.Residents)
                .Where(r=>r.ID ==roomId).FirstOrDefaultAsync();
            return room;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Residents)
                .ToListAsync();
            return rooms;
        }

        public async Task UpdateRoom(Room room)
        {
            _context.Rooms
                .Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(long id)
        {
            var roomToDelete = await GetRoom(id);
            _context.Rooms.Remove(roomToDelete);
            await _context.SaveChangesAsync();
        }

        public Task<List<Room>> GetRoomsForRatOwners()
        {
            var roomsForRatOwner = _context.Rooms
                .Where(r => r.Residents.All(s => s.PetType != PetType.Owl && s.PetType != PetType.Cat))
                .Include(r=>r.Residents)
                .ToListAsync();
            return roomsForRatOwner;
        }
    }
}
