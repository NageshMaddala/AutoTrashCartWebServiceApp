using Newtonsoft.Json;

namespace AutoTrashCartWebServiceApp.Models
{
    public class Sync
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        public Schedule Schedule { get; set; }

        public Path Path { get; set; }

        public Sync()
        {
        }

        public Sync(string token, Schedule schedule, Path path)
        {
            Token = token;
            Schedule = schedule;
            Path = path;
        }
    }
}