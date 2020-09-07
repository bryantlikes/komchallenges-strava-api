// Copyright (c) Bryant Likes. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KomChallenges.StravaApi
{
    public class StravaActivity
    {
        [JsonPropertyName("id")]
        public long ActivityId { get; set; }
        public StravaId Athlete { get; set; }
        public string Name { get; set; }
        public decimal Distance { get; set; }
        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }
        public string Type { get; set; }
        [JsonPropertyName("embed_token")]
        public string EmbedToken { get; set; }
        [JsonPropertyName("total_elevation_gain")]
        public decimal TotalElevationGain { get; set; }
        [JsonPropertyName("average_speed")]
        public decimal AverageSpeed { get; set; }
        [JsonPropertyName("achievement_count")]
        public int AchievementCount { get; set; }
        [JsonPropertyName("kudos_count")]
        public int KudosCount { get; set; }
        [JsonPropertyName("comment_count")]
        public int CommentCount { get; set; }
        [JsonPropertyName("elapsed_time")]
        public int ElapsedTime { get; set; }
        [JsonPropertyName("moving_time")]
        public int MovingTime { get; set; }
        public string Visibility { get; set; }
        public bool Flagged { get; set; }
        public decimal Calories { get; set; }
        public StravaMap Map { get; set; }
        [JsonPropertyName("segment_efforts")]
        public IEnumerable<StravaSegmentEffort> SegmentEfforts { get; set; }
    }
}
