// Copyright (c) Bryant Likes. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Text.Json.Serialization;

namespace KomChallenges.StravaApi
{
    public class StravaSegmentEffort
    {
        public StravaId Segment { get; set; }
        [JsonPropertyName("id")]
        public long EffortId { get; set; }
        [JsonPropertyName("elapsed_time")]
        public int ElapsedTime { get; set; }
        [JsonPropertyName("moving_time")]
        public int MovingTime { get; set; }
        public decimal Distance { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("pr_rank")]
        public int? PrRank { get; set; }
        [JsonPropertyName("kom_rank")]
        public int? KomRank { get; set; }
    }
}
