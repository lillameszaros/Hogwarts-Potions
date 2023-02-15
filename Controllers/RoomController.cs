using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Room>> GetAllRooms()
        {
            return await _service.GetAllRooms();

        }

        [HttpPost]
        public async Task AddRoom([FromBody] Room room)
        {
            await _service.AddRoom(room);
        }

        [HttpGet("{id}")]
        public async Task<Room> GetRoomById(long id)
        {
            return await _service.GetRoom(id);
        }

        [HttpPut("{id}")]
        public async void UpdateRoomById(long id, [FromBody] Room updatedRoom)
        {
            await _service.UpdateRoom(updatedRoom);
        }

        [HttpDelete("{id}")]
        public async Task DeleteRoomById(long id)
        {
            await _service.DeleteRoom(id);
        }

        [HttpGet("rat-owners")]
        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _service.GetRoomsForRatOwners();
        }
    }
}
