using System;
using System.Collections.Generic;
using System.Linq;
using api.domain;
using db.gardens;
using db.sensors;
using Microsoft.AspNetCore.Mvc;

namespace api.repositories
{
    public interface IGardenRepository : IRepository
    {
        Garden GetByName(string name, string userID);
        void CreateGarden(Garden garden, string userID);
        void DeleteGarden(Garden garden, string userID);
        List<Garden> GetAllGardens(string accountID);
        void AddPlantGroup(Garden garden, PlantGroup plantGroup, string userID);
        void DeleteByName(string name, string accountID);
    }
    public class GardenRepository : IGardenRepository
    {
        public IPlantGroupRepository PlantGroupRepository { get; }
        public GardenRepository(IPlantGroupRepository plantGroupRepository)
        {
            PlantGroupRepository = plantGroupRepository;
        }


        public void AddPlantGroup(Garden garden, PlantGroup plantGroup, string userID)
        {
        }

        public void CreateGarden(Garden garden, string userID)
        {
            Gardens gardenDB = new Gardens();
            gardenDB.AddGarden(userID, garden.Id, garden.Name);
        }

        public void DeleteGarden(Garden garden, string userID)
        {
            Gardens gardenDB = new Gardens();
            gardenDB.DeleteGarden(userID, garden.Name);
        }


        public List<Garden> GetAllGardens(string accountID)
        {
            Gardens gardenDB = new Gardens();
            List<string> gardenNames = gardenDB.ListGardens(accountID);
            List<Garden> gardens = gardenNames.Select(name => GetByName(name, accountID)).ToList();
            return gardens;
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

        public void DeleteByName(string name, string accountID)
        {
            Gardens gardenDB = new Gardens();
            gardenDB.DeleteGarden(accountID, name);
        }
    }
}
