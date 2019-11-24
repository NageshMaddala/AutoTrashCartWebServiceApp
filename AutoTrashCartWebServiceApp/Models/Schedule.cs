namespace AutoTrashCartWebServiceApp.Models
{
    public class Schedule
    {
        public string Token { get; set; }
        public int Day { get; set; }
        public System.TimeSpan Pickup { get; set; }
        public bool Holidays { get; set; }
    }
}