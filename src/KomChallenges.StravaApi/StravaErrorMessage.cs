namespace KomChallenges.StravaApi
{
    public class StravaErrorMessage
    {
        public string Message { get; set; }
        public StravaError[] Errors { get; set; }
    }
}
