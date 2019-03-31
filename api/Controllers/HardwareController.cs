using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.domain;
using api.repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        public IPlantGroupRepository PlantGroupRepository { get; }

        public HardwareController(IPlantGroupRepository plantGroupRepository)
        {
            PlantGroupRepository = plantGroupRepository;
        }

        //TODO: add get plant group to hardware

        #region POST
        [HttpPost("plantgroup/hardware/light")]
        public void PostLightSensor(Guid HardwareMAC, int UTCTime ,int SensorValue)
        {
            //TODO: Assign dynamic id for hardware
            int SensorID = 0;

            PlantGroupRepository.InsertLightData(HardwareMAC,UTCTime,SensorID,SensorValue);

        }

        [HttpPost("/plantGroup/hardware/temperature")]
        public void PostTemperatureSensor(Guid HardwareMAC, int UTCTime, int SensorValue)
        {
            //TODO: Assign dynamic id for hardware
            int SensorID = 0;

            PlantGroupRepository.InsertTemperatureData(HardwareMAC, UTCTime, SensorID, SensorValue);
        }

        [HttpPost("/plantGroup/hardware/humidity")]
        public void PostHumidity(Guid HardwareMAC, int UTCTime, int SensorValue)
        {
            //TODO: Assign dynamic id for hardware
            int SensorID = 0;

            PlantGroupRepository.InsertHumidityData(HardwareMAC, UTCTime, SensorID, SensorValue);
        }
        public void PostTemperatureSensor(Guid HardwareMAC, int UTCTime)
        {
            //TODO: Assign static sensorType
            //TODO: Assign static id for hardware
        }

        #endregion
    }
}
