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
        [HttpPost("plantgroup/{plantgroupname}/hardware")]
        public void PostLightSensor(Guid HardwareMAC, int UTCTime ,int SensorValue, string plantgroupname)
        {
            if (String.IsNullOrEmpty(plantgroupname))
            {
                throw new System.ArgumentNullException(nameof(plantgroupname));
            }
            //TODO: Assign dynamic id for hardware
            int SensorID = 0;

            PlantGroupRepository.InsertLightData(HardwareMAC,UTCTime,SensorID,SensorValue);

        }

        [HttpPost("/plantGroup/hardware")]
        public void PostTemperatureSensor(Guid HardwareMAC, int UTCTime, int SensorValue, string plantgroupname)
        {
            //TODO: Assign dynamic id for hardware
            int SensorID = 0;

            PlantGroupRepository.InsertTemperatureData(HardwareMAC, UTCTime, SensorID, SensorValue);

        }

        [HttpPost("/plantGroup/hardware")]
        public void PostHumidity(Guid HardwareMAC, int UTCTime, int SensorValue, string plantgroupname)
        {
            //TODO: Assign dynamic id for hardware
            int SensorID = 0;

            PlantGroupRepository.InsertHumidityData(HardwareMAC, UTCTime, SensorID, SensorValue);

        }
        #endregion
    }
}
