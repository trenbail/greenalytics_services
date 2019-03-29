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
        public void PostLightSensor(Guid HardwareMAC, int UTCTime , string plantgroupname)
        {
            if (String.IsNullOrEmpty(plantgroupname))
            {
                throw new System.ArgumentNullException(nameof(plantgroupname));
            }
            //TODO: Assign static sensorType
            //TODO: Assign static id for hardware

            PlantGroup plantGroup = PlantGroupRepository.GetByName(plantgroupname);





        }

        [HttpPost("/plantGroup/hardware")]
        public void PostTemperatureSensor(Guid HardwareMAC, int UTCTime, string plantgroupname)
        {
            //TODO: Assign static sensorType
            //TODO: Assign static id for hardware
        }

        [HttpPost("/plantGroup/hardware")]
        public void PostHumidity(Guid HardwareMAC, int UTCTime , string plantgroupname)
        {
            //TODO: Assign static sensorType
            //TODO: Assign static id for hardware

        }
        #endregion
    }
}
