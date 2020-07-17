namespace KomChallenges.StravaApi
{
    public static class StravaApiUrls
    {
        public static readonly string Base = "https://www.strava.com/api/v3";
        public static readonly string Athlete = "/athlete";
        public static readonly string Activity = "/activities/{0}?include_all_efforts=true";
        public static readonly string Activities = "/athlete/activities?before={0}&after={1}&page={2}";
        public static readonly string Segment = "/segments/{0}";
        public static readonly string Route = "/routes/{0}";
        public static readonly string Routes = "/athletes/{0}/routes?page={1}&per_page={2}";
    }
}
