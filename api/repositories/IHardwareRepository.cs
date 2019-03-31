using api.domain;
using System;

namespace api.repositories
{
    public interface IHardwareRepository
    {
        void InsertTemperatureData(string HardwareMAC, long UTCTime, int SensorValue);
        void InsertLightData(string hardwareMAC, long UTCTime, int SensorValue);
        void InsertHumidityData(string HardwareMAC, long UTCTime, int SensorValue);
    }
}