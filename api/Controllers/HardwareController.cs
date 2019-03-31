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
        [HttpPost("plantgroup/hardware/light")]
        public void PostLightSensor(JObject data)
        {
            var HardwareMac = data["HardwareMAC"].ToString();
            var UTCTime = Convert.ToInt64(data["UTCTime"].ToString());
            var SensorValue = Convert.ToInt32(data["SensorValue"].ToString());
            HardwareRepository.InsertLightData(HardwareMac, UTCTime,SensorValue);
        }

        [HttpPost("/plantGroup/hardware/temperature")]
        public void PostTemperatureSensor(string HardwareMAC, int UTCTime, int SensorValue)
        {
            HardwareRepository.InsertTemperatureData(HardwareMAC, UTCTime, SensorValue);
        }

        [HttpPost("/plantGroup/hardware/humidity")]
        public void PostHumidity(string HardwareMAC, int UTCTime, int SensorValue)
        {
            HardwareRepository.InsertHumidityData(HardwareMAC, UTCTime, SensorValue);
        }

        #endregion
    }
}
