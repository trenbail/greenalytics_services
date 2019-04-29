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
        readonly string body = "this is a water notification";
        readonly string title = "this is a water notification title";
        private string token;

        public WaterMessage(string token)
        {
            this.token = token;
        }

        public override JObject ToJSON()
        {
            var jo = new JObject();
            jo.Add("to", this.token);
            jo.Add("title", this.title);
            jo.Add("body", this.body);
            return jo;
        }
    }
}