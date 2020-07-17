using System.Text.Json.Serialization;

namespace KomChallenges.StravaApi
{
    public class StravaSegment
    {
        [JsonPropertyName("id")]
        public long SegmentId { get; set; }
        public string Name { get; set; }
        public decimal Distance { get; set; }
        [JsonPropertyName("average_grade")]
        public decimal AverageGrade { get; set; }
        [JsonPropertyName("maximum_grade")]
        public decimal MaximumGrade { get; set; }
        [JsonPropertyName("elevation_high")]
        public decimal ElevationHigh { get; set; }
        [JsonPropertyName("elevation_low")]
        public decimal ElevationLow { get; set; }
        [JsonPropertyName("climb_category")]
        public int ClimbCategory { get; set; }
        [JsonPropertyName("total_elevation_gain")]
        public decimal TotalElevationGain { get; set; }
        public StravaMap Map { get; set; }
    }
}
