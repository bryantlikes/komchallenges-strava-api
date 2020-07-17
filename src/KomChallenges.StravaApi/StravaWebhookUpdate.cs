// Copyright (c) Bryant Likes. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace KomChallenges.StravaApi
{
    public class StravaWebhookUpdate
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public bool? Authorized { get; set; }
    }
}
