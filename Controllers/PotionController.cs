using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public PotionController(IPotionService potionService, IMapper mapper)
        {
            _potionService= potionService;
            _mapper= mapper;
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
            //var potion = _mapper.Map<Potion>(potionDto);
            await _potionService.AddPotion(potion);
            return potion;
        }

        [HttpPost("brew")]
        public async Task<Potion> ExploratoryBrewing(Potion potion)
        {
            var exploratoryPotion = await _potionService.ExploratoryBrew(potion);
            return exploratoryPotion;

        }

        [HttpPut("{potionId}/add")]
        public async Task<Potion> AddIngredientToPotion(int potionId, Ingredient ingredient)
        {
            await _potionService.AddIngredientToPotion(potionId, ingredient);
            var potion = await _potionService.GetPotionById(potionId);
            return potion;
            
        }

        [HttpGet("{potionId}/help")]
        public async Task<List<Recipe>> HelpBrew(int potionId)
        {
            var recipes = await _potionService.HelpBrew(potionId);
            return recipes;
        }
    }
}
