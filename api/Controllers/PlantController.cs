using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.domain;
using api.repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/{accountID}")]
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
            if (plant != null)
            {
                return plant;
            }
            return NotFound();
        }

        [HttpGet("plantGroup/{name}")]
        public ActionResult<PlantGroup> GetPlantGroupByName(string name, string accountID)
        {
            var plantGroup = PlantGroupRepository.GetByName(name, accountID);
            if (plantGroup != null)
            {
                return plantGroup;
            }
            return NotFound();
        }

        [HttpGet("garden/{name}")] //UNTESTED
        public ActionResult<Garden> GetGardenByName(string name)
        {
            var garden = GardenRepository.GetByName(name);
            if (garden != null)
            {
                return garden;
            }
            return NotFound();
        }

        [HttpGet] //UNTESTED
        public ActionResult<List<Garden>> GetAllGardens(string accountID)
        {
            return GardenRepository.GetAllGardens(accountID);
        }

        [HttpGet("incompatibilities")]
        public ActionResult<Dictionary<Plant, List<IPlantRequirement>>> GetIncompatibilities(string plantGroupName, string plantName, string accountID){
            var plantGroup = PlantGroupRepository.GetByName(plantGroupName, accountID);
            if (plantGroup != null)
            {
                return NotFound();
            }
            var plant = PlantRepository.GetByName(plantName);
            if(plant == null)
            {
                return NotFound();
            }
            List<(Plant, List<IPlantRequirement>)> requirementList = plantGroup.GetAllIncompatibilities(plant);
            return requirementList.ToDictionary(tup => tup.Item1, tup => tup.Item2);
        }

        #endregion

        #region POST
        [HttpPost("garden/{gardenName}")]
        public void PostGarden(string gardenName, string userID)
        {
            if (string.IsNullOrEmpty(gardenName))
            {
                throw new ArgumentException("message", nameof(gardenName));
            }

            if (userID == null)
            {
                throw new ArgumentNullException(nameof(userID));
            }

            Garden garden = new Garden(gardenName, userID);
            GardenRepository.CreateGarden(garden);
        }

        [HttpPost("garden/{gardenName}/plantGroup/{plantGroupName}]")]
        public void CreatePlantGroup(string gardenName, string plantGroupName, string userID)
        {
            if (string.IsNullOrEmpty(gardenName))
            {
                throw new ArgumentException("message", nameof(gardenName));
            }

            if (plantGroupName == null)
            {
                throw new ArgumentNullException(nameof(plantGroupName));
            }

            Garden garden = GardenRepository.GetByName(gardenName);

            PlantGroup plantGroup = new PlantGroup(plantGroupName);
            PlantGroupRepository.CreatePlantGroup(garden, plantGroup, userID); 

            garden.AddPlantGroup(plantGroup);
            GardenRepository.AddPlantGroup(garden, plantGroup, userID); //should be update
        }

        [HttpPost("garden/{gardenName}/plantGroup/{plantGroupName}/plant/{plantName}")]
        public void AddPlantToPlantGroup(string gardenName, string plantGroupName, string plantName, string accountID)
        {
            if (string.IsNullOrEmpty(gardenName))
            {
                throw new ArgumentException("message", nameof(gardenName));
            }

            if (string.IsNullOrEmpty(plantGroupName))
            {
                throw new ArgumentException("message", nameof(plantGroupName));
            }

            if (string.IsNullOrEmpty(plantName))
            {
                throw new ArgumentException("message", nameof(plantName));
            }

            PlantGroup plantGroup = PlantGroupRepository.GetByName(plantGroupName, accountID);

            Plant plant = PlantRepository.GetByName(plantName);

            plantGroup.AddPlant(plant);
            PlantGroupRepository.AddPlantToPlantGroup(plantGroup, plant, accountID); //should be update
        }

        #endregion
    }
}
