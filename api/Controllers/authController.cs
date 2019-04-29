using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using api.domain;
using api.repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IPlantRepository PlantRepository { get; }
        public IPlantGroupRepository PlantGroupRepository { get; }

        public AuthController(IPlantRepository plantRepository,
            IPlantGroupRepository plantGroupRepository)
        {
            PlantRepository = plantRepository;
            PlantGroupRepository = plantGroupRepository;
        }

        #region POST
        [HttpPost("water")]
        public void SendWaterReminders()
        {
            //pgName, token
            List<(String, String)> outOfDateList = PlantGroupRepository.GatherWaterNotifications(DateTime.Now);
            foreach (var tup in outOfDateList)
            {
                var (pgName, token) = tup;
                sendNotification(new WaterMessage(token));
            }
        }

        private async void sendNotification(Message message)
        {
            var url = "https://exp.host/--/api/v2/push/send";
            HttpClient client = new HttpClient();

            var jdata = message.ToJSON();
            var httpContent = new StringContent(jdata.ToString());
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync(url, httpContent);
    }

    #endregion
}
}
