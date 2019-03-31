using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.domain;
using api.repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        public IHardwareRepository HardwareRepository { get; }

        public HardwareController(IHardwareRepository hardwareRepository)
        {
            HardwareRepository = hardwareRepository;
        }

        //TODO: add get plant group to hardware

        #region POST
        [HttpPost("/hardware/light")]
        public void PostLightSensor(JObject data)
        {
            var HardwareMAC = data["HardwareMAC"].ToString();
            var UTCTime = Convert.ToInt64(data["UTCTime"].ToString());
            var SensorValue = Convert.ToInt32(data["SensorValue"].ToString());
            HardwareRepository.InsertLightData(HardwareMAC, UTCTime,SensorValue);
        }

        [HttpPost("/hardware/temperature")]
        public void PostTemperatureSensor(JObject data)
        {
            var HardwareMAC = data["HardwareMAC"].ToString();
            var UTCTime = Convert.ToInt64(data["UTCTime"].ToString());
            var SensorValue = Convert.ToInt32(data["SensorValue"].ToString());
            HardwareRepository.InsertTemperatureData(HardwareMAC, UTCTime, SensorValue);
        }

        [HttpPost("/hardware/humidity")]
        public void PostHumidity(JObject data)
        {
            var HardwareMAC = data["HardwareMAC"].ToString();
            var UTCTime = Convert.ToInt64(data["UTCTime"].ToString());
            var SensorValue = Convert.ToInt32(data["SensorValue"].ToString());
            HardwareRepository.InsertHumidityData(HardwareMAC, UTCTime, SensorValue);
        }

        #endregion
    }
}
