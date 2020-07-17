using System.Text.Json.Serialization;

namespace KomChallenges.StravaApi
{
    public class StravaClub
    {
        [JsonPropertyName("id")]
        public long ClubId { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("profile_medium")]
        public string ProfileMedium { get; set; }
        public bool Private { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
