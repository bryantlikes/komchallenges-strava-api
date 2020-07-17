// Copyright (c) Bryant Likes. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace KomChallenges.StravaApi
{
    public class StravaErrorMessage
    {
        public string Message { get; set; }
        public StravaError[] Errors { get; set; }
    }
}
