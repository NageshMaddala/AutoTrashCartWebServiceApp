using System.Linq;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace AutoTrashCartWebServiceApp.Models
{
    public class Path
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "s")]
        public string S { get; set; }

        [JsonProperty(PropertyName = "e")]
        public string E { get; set; }

        [JsonProperty(PropertyName = "leftb")]
        public string[] Leftb { get; set; }

        [JsonProperty(PropertyName = "rightb")]
        public string[] Rightb { get; set; }

        [JsonProperty(PropertyName = "centerl")]
        public string[] Centerl { get; set; }

        [JsonProperty(PropertyName = "so")]
        public string So { get; set; }

        [JsonProperty(PropertyName = "eo")]
        public string Eo { get; set; }

        public Path()
        {

        }

        public Path(string token, Location[] location, Orientation[] orientation)
        {
            Token = token;
            var spoint = location.FirstOrDefault(l => l.LocationType.Trim() == "s");

            if (spoint != null)
            {
                S = $"{spoint.Latitude0},{spoint.Longitude0}";
            }

            var epoint = location.FirstOrDefault(l => l.LocationType.Trim() == "e");

            if (spoint != null)
            {
                E = $"{epoint.Latitude0},{epoint.Longitude0}";
            }

            Leftb = ReturnPoints("leftb", location);

            Rightb = ReturnPoints("rightb", location);

            Centerl = ReturnPoints("centerl", location);

            So = ReturnPoints("so", orientation);

            Eo = ReturnPoints("eo", orientation);
        }

        public string[] ReturnPoints(string filter, Location[] location)
        {
            var point = location.FirstOrDefault(l => l.LocationType.Trim() == filter);
            string[] leftPoints = new string[3];
            if (point != null)
            {
                leftPoints = new[]
                {
                    $"{point.Latitude0},{point.Longitude0}",
                    $"{point.Latitude1},{point.Longitude1}",
                    $"{point.Latitude2},{point.Longitude2}"
                };
            }

            return leftPoints;
        }

        public string ReturnPoints(string filter, Orientation[] orientations)
        {
            var point = orientations.FirstOrDefault(o => o.OrientationType.Trim() == filter);
            string returnString = "";
            if (point != null)
            {
                returnString = $"{point.X}, {point.Y}, {point.Z}";
            }

            return returnString;
        }
    }
}