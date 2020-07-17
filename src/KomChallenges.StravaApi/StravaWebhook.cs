// Copyright (c) Bryant Likes. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace KomChallenges.StravaApi
{
    public class StravaWebhook
    {
        [JsonPropertyName("object_type")]
        public string ObjectType { get; set; }

        [JsonPropertyName("object_id")]
        public long ObjectId { get; set; }

        [JsonPropertyName("aspect_type")]
        public string AspectType { get; set; }

        [JsonPropertyName("owner_id")]
        public long OwnerId { get; set; }

        [JsonPropertyName("subscription_id")]
        public int SubscriptionId { get; set; }

        [JsonPropertyName("event_time")]
        public int EventTime { get; set; }

        [JsonPropertyName("updates")]
        public StravaWebhookUpdate Updates { get; set; }
    }
}
