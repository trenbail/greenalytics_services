﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.domain;
using api.repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class GreenalyticsController : ControllerBase
    {
        public IPlantRepository PlantRepository { get; }
        public IPlantGroupRepository PlantGroupRepository { get; }
        public IGardenRepository GardenRepository { get; }

        public GreenalyticsController(IPlantRepository plantRepository,
            IPlantGroupRepository plantGroupRepository,
            IGardenRepository gardenRepository)
        {
            PlantRepository = plantRepository;
            PlantGroupRepository = plantGroupRepository;
            GardenRepository = gardenRepository;
        }

        #region GET
        [HttpGet("plant/{name}")]
        public ActionResult<Plant> GetPlantByName(string name)
        {
            var plant = PlantRepository.GetByName(name);
            if(plant == null)
            {
                return plant;
            }
            return NotFound();
        }

        [HttpGet("plantGroup/{name}")]
        public ActionResult<PlantGroup> GetPlantGroupByName(string name)
        {
            var plantGroup = PlantGroupRepository.GetByName(name);
            if (plantGroup != null)
            {
                return plantGroup;
            }
            return NotFound();
        }

        [HttpGet("garden/{name}")]
        public ActionResult<Garden> GetGardenByName(string name)
        {
            var garden = GardenRepository.GetByName(name);
            if (garden != null)
            {
                return garden;
            }
            return NotFound();
        }
        #endregion

        #region POST
        [HttpPost("garden")]
        public void PostGarden([FromBody] Guid AccountID, string name)
        {
            Garden garden = new Garden(name, AccountID);
            if (garden.Id == Guid.Empty || garden.Id == null)
            {
                garden.Id = Guid.NewGuid();
            }
            if (garden.AccountId == Guid.Empty || garden.Id == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(garden.Name))
            {
                return;
            }
            GardenRepository.CreateGarden(garden);
        }

        [HttpPost("plant")]
        public void PostPlant([FromBody] string name)
        {
            Plant plant = new Plant(name);
            if (string.IsNullOrEmpty(plant.Name))
            {
                return;
            }
            PlantRepository.CreatePlant(plant);
        }

        [HttpPost("plantGroup")]
        public void PostPlantGroup([FromBody] string name)
        {
            PlantGroup plantGroup = new PlantGroup(name);
            if (string.IsNullOrEmpty(plantGroup.Name))
            {
                return;
            }
            PlantGroupRepository.CreatePlantGroup(plantGroup);
        }



        #endregion
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}