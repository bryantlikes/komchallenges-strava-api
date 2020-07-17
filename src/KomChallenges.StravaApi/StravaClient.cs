using AspNet.Security.OAuth.Strava;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KomChallenges.StravaApi
{

    public class StravaClient
    {
        private readonly ILogger<StravaClient> _logger;
        private readonly IOptionsMonitor<StravaClientOptions> _options;
        private readonly string DeauthorizeEndpoint = "https://www.strava.com/oauth/deauthorize";

        private static JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public StravaClient(IOptionsMonitor<StravaClientOptions> options, ILogger<StravaClient> logger)
        {
            _logger = logger;
            _options = options;
        }

        public static int FifteenMinuteLimit { get; private set; }
        public static int FifteenMinuteUsed { get; private set; }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _serializeOptions);
        }

        public async Task<StravaAthlete> GetAthlete(string accessToken)
        {
            return await StravaApiCall<StravaAthlete>(StravaApiUrls.Athlete, accessToken);
        }

        public async Task<StravaActivity> GetActivity(long activityId, string accessToken)
        {
            var action = string.Format(StravaApiUrls.Activity, activityId);
            return await StravaApiCall<StravaActivity>(action, accessToken);
        }
        
        public async Task<IEnumerable<StravaActivity>> GetActivities(string accessToken, DateTime startDate, DateTime endDate, int page)
        {
            var action = string.Format(StravaApiUrls.Activities, EpochTimestamp(endDate), EpochTimestamp(startDate), page);
            return await StravaApiCall<List<StravaActivity>>(action, accessToken);
        }

        public async Task<StravaRoute> GetRoute(long routeId, string accessToken)
        {
            var action = string.Format(StravaApiUrls.Route, routeId);
            return await StravaApiCall<StravaRoute>(action, accessToken);
        }

        public async Task<IEnumerable<StravaRoute>> GetRoutes(string accessToken, long athleteId, int page, int pageSize)
        {
            var action = string.Format(StravaApiUrls.Routes, athleteId, page, pageSize);
            return await StravaApiCall<List<StravaRoute>>(action, accessToken);
        }

        public async Task<StravaSegment> GetSegment(long segmentId, string accessToken)
        {
            var action = string.Format(StravaApiUrls.Segment, segmentId);
            return await StravaApiCall<StravaSegment>(action, accessToken);
        }

        public async Task<bool> Deauthorize(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, DeauthorizeEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await GetResponse(request);

            return response.IsSuccessStatusCode;
        }
        
        public async Task<OAuthTokenResponse> RefreshToken(string refreshToken)
        {
            var vals = new Dictionary<string, string>()
            {
                { "client_id", _options.CurrentValue.ClientId },
                { "client_secret", _options.CurrentValue.ClientSecret },
                { "refresh_token", refreshToken },
                { "grant_type", "refresh_token" },
            };

            var requestContent = new FormUrlEncodedContent(vals);
            var response = await GetTokenResponse(requestContent);

            if (response.IsSuccessStatusCode)
            {
                using var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                return OAuthTokenResponse.Success(payload);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                try
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var message = Deserialize<StravaErrorMessage>(json);
                    var err = message.Errors.First();
                    if (err.Resource == "RefreshToken" && err.Code == "invalid")
                    {
                        return OAuthTokenResponse.Failed(new ApplicationException("Refresh Token not valid"));
                    }
                }
                catch
                { }
            }

            var error = "OAuth token endpoint failure: " + await Display(response);
            return OAuthTokenResponse.Failed(new Exception(error));
        }

        private int EpochTimestamp(DateTime date)
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        
        private async Task<T> StravaApiCall<T>(string action, string accessToken) where T : new()
        {
            var requestUrl = $"{StravaApiUrls.Base}{action}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await GetResponse(request);

            return await ProcessStravaResponse<T>(response);
        }

        private async Task<HttpResponseMessage> GetResponse(HttpRequestMessage request)
        {
            var client = HttpClientFactory.Create();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                ReadRateLimitHeaders(response);
            }

            return response;
        }

        private async Task<HttpResponseMessage> GetTokenResponse(FormUrlEncodedContent requestContent)
        {
            var client = HttpClientFactory.Create();
            var response = await client.PostAsync(StravaAuthenticationDefaults.TokenEndpoint, requestContent);

            // NOTE: No rate limit headers on token requests

            return response;
        }

        private async Task<T> ProcessStravaResponse<T>(HttpResponseMessage response) where T : new()
        {
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Deserialize<T>(json);
            }
            else
            {
                _logger.LogWarning("Strava API failure: " + await Display(response));
                return default(T);
            }
        }

        private void ReadRateLimitHeaders(HttpResponseMessage response)
        {
            try
            {
                var limit = response.Headers.GetValues("X-Ratelimit-Limit").FirstOrDefault();
                var used = response.Headers.GetValues("X-Ratelimit-Usage").FirstOrDefault();

                if (limit != null && used != null)
                {
                    FifteenMinuteLimit = int.Parse(limit.Split(',').First());
                    FifteenMinuteUsed = int.Parse(used.Split(',').First());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process rate limit headers");
            }
        }

        private static async Task<string> Display(HttpResponseMessage response)
        {
            var output = new StringBuilder();
            output.Append("Status: " + response.StatusCode + ";");
            output.Append("Headers: " + response.Headers.ToString() + ";");
            output.Append("Body: " + await response.Content.ReadAsStringAsync() + ";");
            return output.ToString();
        }
    }
}
