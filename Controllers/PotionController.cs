using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/potions")]
    public class PotionController : ControllerBase
    {
        private readonly IPotionService _potionService;

        public PotionController(IPotionService potionService)
        {
            _potionService= potionService;
        }

        [HttpGet]
        public async Task<List<Potion>> GetAllPotion()
        {
            return await _potionService.GetAllPotion();
        }

        [HttpGet("{studentId}")]
        public async Task<List<Potion>> GetExploredPotionsOfStudent(int studentId)
        {
            return await _potionService.ExploredPotionsByStudent(studentId);
        }

        [HttpPost]
        public async Task<Potion> AddPotion(Potion potion)
        {
            await _potionService.AddPotion(potion);
            return potion;
        }
    }
}
