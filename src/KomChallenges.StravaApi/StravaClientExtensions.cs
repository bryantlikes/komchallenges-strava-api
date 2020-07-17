using KomChallenges.StravaApi;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StravaClientExtensions
    {
        public static IServiceCollection AddStravaClient(this IServiceCollection services, Action<StravaClientOptions> options)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddOptions();

            if (options != null)
            {
                services.PostConfigure(options);
            }

            services.AddHttpClient();
            services.AddScoped<StravaClient>();

            return services;
        }
    }
}
