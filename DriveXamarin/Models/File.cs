using Newtonsoft.Json;

namespace DriveXamarin.Models
{
    public class File
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("path")]
        public string path { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
    }
}