using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KomChallenges.StravaApi
{
    public class StravaRoute
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Private { get; set; }
        /// <summary>
        ///  (1 for ride, 2 for runs)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        ///  (1 for road, 2 for mountain bike, 3 for cross, 4 for trail, 5 for mixed)
        /// </summary>
        [JsonPropertyName("sub_type")]
        public int SubType { get; set; }
        public StravaMap Map { get; set; }
        public IEnumerable<StravaSegment> Segments { get; set; }
    }
}
