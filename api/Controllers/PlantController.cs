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
        public ActionResult<Garden> GetGardenByName(string name, string accountID)
        {
            var garden = GardenRepository.GetByName(name, accountID);
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

        [HttpGet("incompatibilities/plantGroup/{plantGroupName}/plant/{plantName}")]
        public ActionResult<(string, string)> GetIncompatibilities(string plantGroupName, string plantName, string accountID){
            var plantGroup = PlantGroupRepository.GetByName(plantGroupName, accountID);
            if (plantGroup == null)
            {
                return NotFound();
            }
            var plant = PlantRepository.GetByName(plantName);
            if(plant == null)
            {
                return NotFound();
            }
            List<(Plant, List<IPlantRequirement>)> requirementList = plantGroup.GetAllIncompatibilities(plant);

            List<(string, string)> expandedList = new List<(string, string)>();
            foreach((Plant, List<IPlantRequirement>) reqPair in requirementList)
            {
                foreach(IPlantRequirement req in reqPair.Item2)
                {
                    expandedList.Add((reqPair.Item1.Name, req.TypeOfReq()));
                }
            }
            return Ok(expandedList);
        }

        #endregion

        #region POST
        [HttpPost("garden/{gardenName}")]
        public void PostGarden(string accountID, string gardenName)
        {
            if (string.IsNullOrEmpty(gardenName))
            {
                throw new ArgumentException("message", nameof(gardenName));
            }

            if (accountID == null)
            {
                throw new ArgumentNullException(nameof(accountID));
            }

            Garden garden = new Garden(gardenName, accountID);
            GardenRepository.CreateGarden(garden, accountID);
        }

        [HttpPost("garden/{gardenName}/plantGroup/{plantGroupName}")]
        public void CreatePlantGroup(string gardenName, string plantGroupName, string accountID)
        {
            if (string.IsNullOrEmpty(gardenName))
            {
                throw new ArgumentException("message", nameof(gardenName));
            }

            if (plantGroupName == null)
            {
                throw new ArgumentNullException(nameof(plantGroupName));
            }

            Garden garden = GardenRepository.GetByName(gardenName, accountID);

            PlantGroup plantGroup = new PlantGroup(plantGroupName);
            PlantGroupRepository.CreatePlantGroup(garden, plantGroup, accountID); 

            garden.AddPlantGroup(plantGroup);
            GardenRepository.AddPlantGroup(garden, plantGroup, accountID); //should be update
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

            Garden garden = GardenRepository.GetByName(gardenName, accountID);

            PlantGroup plantGroup = PlantGroupRepository.GetByName(plantGroupName, accountID);

            Plant plant = PlantRepository.GetByName(plantName);

            plantGroup.AddPlant(plant);
            PlantGroupRepository.AddPlantToPlantGroup(plantGroup, plant, accountID); //should be update
        }

        #endregion
    }
}
