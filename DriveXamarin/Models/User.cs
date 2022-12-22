using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DriveXamarin.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("userName")]
        public string Username { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("drivingSchool")]
        public string DrivingSchool { get; set; }
        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; }
        [JsonProperty("errorQuestion")]
        public List<Question> ErrorQuestion { get; set; }
        [JsonProperty("points")]
        public long Points { get; set; }

    }
}
