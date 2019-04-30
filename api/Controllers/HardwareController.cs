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
    [Route("api/hardware")]
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
        [HttpPost("light")]
        public void PostLightSensor(JObject data)
        {
            var HardwareMAC = data["HardwareMAC"].ToString();
            var UTCTime = Convert.ToInt64(data["UTCTime"].ToString());
            var SensorValue = Convert.ToInt32(data["SensorValue"].ToString());
            HardwareRepository.InsertLightData(HardwareMAC, UTCTime,SensorValue);
        }

        [HttpPost("temperature")]
        public void PostTemperatureSensor(JObject data)
        {
            var HardwareMAC = data["HardwareMAC"].ToString();
            var UTCTime = Convert.ToInt64(data["UTCTime"].ToString());
            var SensorValue = Convert.ToInt32(data["SensorValue"].ToString());
            HardwareRepository.InsertTemperatureData(HardwareMAC, UTCTime, SensorValue);
        }

        [HttpPost("humidity")]
        public void PostHumidity(JObject data)
        {
            var HardwareMAC = data["HardwareMAC"].ToString();
            var UTCTime = Convert.ToInt64(data["UTCTime"].ToString());
            var SensorValue = Convert.ToInt32(data["SensorValue"].ToString());
            HardwareRepository.InsertHumidityData(HardwareMAC, UTCTime, SensorValue);
        }
        #endregion

        #region GET
        [HttpGet("{HardwareMAC}/humidity/{since}")]
        public ActionResult<List<long>> GetHumidityByMAC(string HardwareMAC, int since)
        {
            List<List<long>> data = HardwareRepository.GetHumidityByMAC(HardwareMAC, since);
            if (data.Count == 0)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpGet("{HardwareMAC}/light/{since}")]
        public ActionResult<List<long>> GetLightByMAC(string HardwareMAC, int since)
        {
            List<List<long>> data = HardwareRepository.GetLightByMAC(HardwareMAC, since);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpGet("{HardwareMAC}/temperature/{since}")]
        public ActionResult<List<long>> GetTemperatureByMAC(string HardwareMAC, int since)
        {
            List<List<long>> data = HardwareRepository.GetTemperatureByMAC(HardwareMAC, since);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpGet("{accountID}/{pgName}/humidity/{since}")]
        public ActionResult<List<long>> GetHumidityByPG(string accountID, string pgName, int since)
        {
            string mac = HardwareRepository.GetMacByPlantGroupName(accountID, pgName);
            if (String.IsNullOrEmpty(mac))
            {
                return NotFound();
            }
            return GetHumidityByMAC(mac, since);
        }

        [HttpGet("{accountID}/{pgName}/light/{since}")]
        public ActionResult<List<long>> GetLightByPG(string accountID, string pgName, int since)
        {
            string mac = HardwareRepository.GetMacByPlantGroupName(accountID, pgName);
            if (String.IsNullOrEmpty(mac))
            {
                return NotFound();
            }
            return GetLightByMAC(mac, since);
        }
        [HttpGet("{accountID}/{pgName}/temperature/{quantity}")]
        public ActionResult<List<long>> GetTemperatureByPG(string accountID, string pgName, int since)
        {
            string mac = HardwareRepository.GetMacByPlantGroupName(accountID, pgName);
            if (String.IsNullOrEmpty(mac))
            {
                return NotFound();
            }
            return GetTemperatureByMAC(mac, since);
        }
        #endregion
    }
}
