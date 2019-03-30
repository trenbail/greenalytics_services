using api.domain;
using System;

namespace api.repositories
{
    public interface IPlantGroupRepository
    {
        PlantGroup GetByName(string name, string userID);
        void InsertTemperatureData(Guid hardwareMAC,int UTCTime, int SensorID ,int SensorValue);
        void InsertLightData(Guid hardwareMAC, int UTCTime, int SensorID, int SensorValue);
        void InsertHumidityData(Guid hardwareMAC, int UTCTime, int SensorID, int SensorValue);

        void CreatePlantGroup(Garden garden, PlantGroup plantGroup, string userID);
        void AddPlantToPlantGroup(PlantGroup plantGroup, Plant plant, string userID);
    }
}