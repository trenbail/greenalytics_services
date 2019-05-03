using api.domain;
using db.sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace api.repositories
{
    public interface IPlantGroupRepository
    {
        PlantGroup GetByName(string name, string userID);
        void CreatePlantGroup(Garden garden, PlantGroup plantGroup, string userID);
        void DeletePlantGroup(Garden garden, PlantGroup plantGroup, string userID);
        void AddPlantToPlantGroup(PlantGroup plantGroup, Plant plant, string userID);
        void DeletePlantFromPlantGroup(PlantGroup plantGroup, Plant plant, string userID);
        List<(string, string)> GatherWaterNotifications(DateTime now);
        void AddHardwareToPlantGroup(string accountID, string plantGroupName, string hardwareID);
        void RemoveHardwareFromPlantGroup(string accountID, string plantGroupName, string hardwareID);
        List<List<string>> GetAllHardware(string accountID);
        void DeleteByName(string gardenName, string groupName, string accountID);
        void DeletePlantFromPlantGroupByName(string accountID, string gardenName, string plantGroupName, string plantName);
    }
    public class PlantGroupRepository : IPlantGroupRepository
    {
        private readonly IHardwareRepository hardwareRepository;

        public IPlantRepository PlantRepo { get; }
        public PlantGroupRepository(IPlantRepository plantRepo, IHardwareRepository hardwareRepository)
        {
            PlantRepo = plantRepo;
            this.hardwareRepository = hardwareRepository;
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

        public void DeletePlantGroup(Garden garden, PlantGroup plantGroup, string userID)
        {
            var groupDB = new db.groups.Groups();
            groupDB.DeleteGroup(userID,garden.Name,plantGroup.Name);
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
            int temp = new Sensors().getLatestTemperature(userID, name);
            int humidity = new Sensors().getLatestHumidity(userID, name);

            plantGroup.summary = new Summary(temp, humidity);
            return plantGroup;
        }

        public List<(string, string)> GatherWaterNotifications(DateTime now)
        {
            var groupDB = new db.groups.Groups();
            //userID, plantGroupName, token
            List<(string, string, string)> notificationData = groupDB.getNotificationData();

            groupDB.updateNotification(notificationData.Select(a => (a.Item1, a.Item2)));

            return notificationData.Select(a => (a.Item2, a.Item3)).ToList();
        }

        public void AddHardwareToPlantGroup(string accountID, string plantGroupName, string hardwareID)
        {
            var groupDB = new db.groups.Groups();
            groupDB.AddHardware(accountID, plantGroupName, hardwareID);
        }

        public void RemoveHardwareFromPlantGroup(string accountID, string plantGroupName, string hardwareID)
        {
            var groupDB = new db.groups.Groups();
            groupDB.DeleteHardware_byGroup(accountID, plantGroupName);
        }

        public List<List<string>> GetAllHardware(string accountID)
        {
            var groupDB = new db.groups.Groups();
            return groupDB.ListHardware(accountID);
        }

        public void DeleteByName(string gardenName, string groupName, string accountID)
        {
            var groupDB = new db.groups.Groups();
            groupDB.DeleteGroup(accountID, gardenName, groupName);
        }

        public void DeletePlantFromPlantGroupByName(string accountID, string gardenName, string plantGroupName, string plantName)
        {
            var groupDB = new db.groups.Groups();
            groupDB.DeletePlant(accountID, plantGroupName, plantName);
        }
    }
}
