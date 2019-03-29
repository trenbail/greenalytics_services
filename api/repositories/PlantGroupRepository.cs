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
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void CreatePlantGroup(PlantGroup plantGroup)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public PlantGroup GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void InsertTemperatureData(int UTCTime, string SensorType, int SensorID, int SensorValue)
        {
            throw new NotImplementedException();
        }

        public void InsertLightData(int UTCTime, string SensorType, int SensorID, int SensorValue)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void AddSensor(PlantGroup plantGroup)
        {
            Guid id = plantGroup.getHardwareID();
            string s_id = id.ToString();
            Sensors sensors = new Sensors();
        }

        public void InsertHumidityData(int UTCTime, string SensorType, int SensorID, int SensorValue)
        {
            throw new NotImplementedException();
        }
    }
}
