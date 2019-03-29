using api.domain;

namespace api.repositories
{
    public interface IPlantGroupRepository
    {
        PlantGroup GetByName(string name);
        void CreatePlantGroup(PlantGroup plantGroup);
        void InsertTemperatureData(int UTCTime, string SensorType,int SensorID ,int SensorValue);
        void InsertLightData(int UTCTime, string SensorType, int SensorID, int SensorValue);
        void InsertHumidityData(int UTCTime, string SensorType, int SensorID, int SensorValue);
    }
}