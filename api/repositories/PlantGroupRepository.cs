using api.domain;
using db.sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace api.repositories
{
    public class PlantGroupRepository : IPlantGroupRepository
    {
        public IPlantRepository PlantRepo { get; }
        public PlantGroupRepository(IPlantRepository plantRepo)
        {
            PlantRepo = plantRepo;
        }

        public void AddPlantToPlantGroup(PlantGroup plantGroup, Plant plant, string userID)
        {
            var groupDB = new db.groups.Groups();
            groupDB.AddPlant(userID, plantGroup.Name, plant.Name);
        }

        public void DeletePlantFromPlantGroup(PlantGroup plantGroup, Plant plant, string userID)
        {
            var groupDB = new db.groups.Groups();
            groupDB.DeletePlant(userID, plantGroup.Name, plant.Name);
        }

        public void AddSensor(PlantGroup plantGroup)
        {
            Guid id = plantGroup.getHardwareID();
            string s_id = id.ToString();
            Sensors sensors = new Sensors();
        }

        public void CreatePlantGroup(Garden garden, PlantGroup plantGroup, string userID)
        {
            var groupDB = new db.groups.Groups();
            groupDB.AddGroup(userID, garden.Name, plantGroup.Id, plantGroup.Name);
            groupDB.initNotifications(userID, plantGroup.Name);
            return;
        }

        public PlantGroup GetByName(string name, string userID)
        {
            var plantGroup = new PlantGroup(name);

            var groupDB = new db.groups.Groups();
            List<string> plantList = groupDB.ListPlants(userID, name);
            if(plantList == null)
            {
                throw new Exception();
            }
            plantList.Select(plantName => PlantRepo.GetByName(plantName))
                .ToList()
                .ForEach(plant => plantGroup.AddPlant(plant));
            //TODO: Add Hardware
            return plantGroup;
        }

        public List<(string, string)> GatherWaterNotifications(DateTime now)
        {
            var groupDB = new db.groups.Groups();
            //userID, plantGroupName, token
            List<(string, string, string)> notificationData = groupDB.getNotificationData();

            //groupDB.updateNotification(notificationData.Select(a => (a.Item1, a.Item2)));

            return notificationData.Select(a => (a.Item2, a.Item3)).ToList();
        }
    }
}
