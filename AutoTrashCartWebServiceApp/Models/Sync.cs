using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AutoTrashCartWebServiceApp.Models
{
    public class Sync
    {
        [JsonProperty(PropertyName = "syncId")]
        [Required]
        public string SyncId { get; set; }

        public Schedule Schedule { get; set; }

        public Path Path { get; set; }

        public Sync()
        {
        }

        public Sync(string token, Schedule schedule, Path path)
        {
            SyncId = token;
            Schedule = schedule;
            Path = path;
        }
    }
}