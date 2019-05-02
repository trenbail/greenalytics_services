using api.domain;
using db.sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace api.repositories
{
    public interface IHardwareRepository
    {
        void InsertTemperatureData(string HardwareMAC, long UTCTime, int SensorValue);
        void InsertLightData(string hardwareMAC, long UTCTime, int SensorValue);
        void InsertHumidityData(string HardwareMAC, long UTCTime, int SensorValue);
        List<List<long>> GetHumidityByMAC(string hardwareMAC, int since);
        List<List<long>> GetLightByMAC(string hardwareMAC, int since);
        List<List<long>> GetTemperatureByMAC(string hardwareMAC, int since);
        string GetMacByPlantGroupName(string accountID, string pgName);
    }

    public class HardwareRepository : IHardwareRepository
    {
        public List<List<long>> GetDataByMacANDSensorType(string hardwareMAC, int since, string sensorType)
        {
            var SensorDB = new Sensors();
            return SensorDB.pulldata(hardwareMAC, sensorType, since);
        }

        public List<List<long>> GetHumidityByMAC(string hardwareMAC, int since)
        {
            return GetDataByMacANDSensorType(hardwareMAC, since, "humidity");
        }

        public List<List<long>> GetLightByMAC(string hardwareMAC, int since)
        {
            return GetDataByMacANDSensorType(hardwareMAC, since, "light");
        }


        public List<List<long>> GetTemperatureByMAC(string hardwareMAC, int since)
        {
            return GetDataByMacANDSensorType(hardwareMAC, since, "temperature");
        }

        public string GetMacByPlantGroupName(string accountID, string pgName)
        {
            var SensorDB = new Sensors();
            return SensorDB.getMacByAcctANDpgName(accountID, pgName);
        }

        public void InsertHumidityData(string HardwareMAC, long UTCTime, int SensorValue)
        {
            //ADD TYPE
            string SensorType = "humidity";
            Sensors HumiditySensor = new Sensors();
            HumiditySensor.Insert(HardwareMAC,UTCTime,SensorType,SensorValue);

        }

        public void InsertLightData(string HardwareMAC, long UTCTime, int SensorValue)
        {
            string SensorType = "light";
            Sensors LightSensor = new Sensors();
            LightSensor.Insert(HardwareMAC, UTCTime, SensorType, SensorValue);
        }

        public void InsertTemperatureData(string HardwareMAC, long UTCTime, int SensorValue)
        {
            string SensorType = "temperature";
            Sensors TemperatureSensor = new Sensors();
            TemperatureSensor.Insert(HardwareMAC, UTCTime, SensorType, SensorValue);
        }
    }
}
