using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace api.Controllers
{
    abstract public class Message
    {
        string body;
        string title;
        abstract public JObject ToJSON();
    }
    public class WaterMessage : Message
    {
        readonly string body = " needs water!";
        readonly string title = "this is a water notification title";
        private string token;

        public WaterMessage(string token, string pg)
        {
            this.token = token;
            this.body = pg + body;
        }

        public override JObject ToJSON()
        {
            var jo = new JObject();
            var to = "ExponentPushToken["+this.token+"]";
            jo.Add("title", this.title);
            jo.Add("body", this.body);
            jo.Add("to", to);
            return jo;
        }
    }
}