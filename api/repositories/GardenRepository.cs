using System;
using System.Collections.Generic;
using System.Linq;
using api.domain;
using db.gardens;
using db.sensors;
using Microsoft.AspNetCore.Mvc;

namespace api.repositories
{
    public class GardenRepository : IGardenRepository
    {
        public IPlantGroupRepository PlantGroupRepository { get; }
        public GardenRepository(IPlantGroupRepository plantGroupRepository)
        {
            PlantGroupRepository = plantGroupRepository;
        }


        public void AddPlantGroup(Garden garden, PlantGroup plantGroup, string userID)
        {
            //
        }

        public void CreateGarden(Garden garden, string userID)
        {
            Gardens gardenDB = new Gardens();
            gardenDB.AddGarden(userID, garden.Id, garden.Name);
        }

        public ActionResult<List<Garden>> GetAllGardens(string accountID)
        {
            throw new NotImplementedException();
        }

       
        public Garden GetByName(string gardenName, string userID)
        {
            Garden garden = new Garden(gardenName, userID);
            Gardens gardenDB = new Gardens();
            var names = gardenDB.ListGroups(userID, gardenName);
            names.Select(name => PlantGroupRepository.GetByName(name, userID)).ToList()
                .ForEach(garden.AddPlantGroup);
            return garden;
        }
    }
}
