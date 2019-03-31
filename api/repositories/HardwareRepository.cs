using api.domain;
using db.sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace api.repositories
{
    public class HardwareRepository : IHardwareRepository
    {
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
