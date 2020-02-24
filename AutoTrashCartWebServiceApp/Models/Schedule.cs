using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AutoTrashCartWebServiceApp.Models
{
    public class Schedule
    {
        [JsonProperty(PropertyName = "scheduleId")]
        [Required]
        public string ScheduleId { get; set; }

        public int Day { get; set; }

        public System.TimeSpan Pickup { get; set; }

        public bool Holidays { get; set; }
    }
}