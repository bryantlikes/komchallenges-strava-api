// Copyright (c) Bryant Likes. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KomChallenges.StravaApi
{
    public class StravaAthlete
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Sex { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public string Profile { get; set; }
        [JsonPropertyName("profile_medium")]
        public string ProfileMedium { get; set; }
        public IEnumerable<StravaClub> Clubs { get; set; }
    }
}
