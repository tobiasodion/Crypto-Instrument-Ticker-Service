using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TickerSubscription.Settings;
using TickerSubscription.Settings;

namespace TickerSubscription.Extensions;

/// <summary>
/// Configuration Settings-related extensions for the Web Application Builder.
/// </summary>
public static class WebApplicationBuilderConfigurationExtensions
{
    /// <summary>
    /// Adds all configuration settings to the Web Application Builder.
    /// </summary>
    /// <param name="builder">The web application builder</param>
    public static void AddConfigurationSettings(this WebApplicationBuilder builder)
    {
        builder.AddSettingsInstance<ConnectionCheckSettings>(nameof(ConnectionCheckSettings));
        builder.AddSettingsInstance<SubscriptionSettings>(nameof(SubscriptionSettings));
        builder.AddSettingsInstance<WebSocketSettings>(nameof(WebSocketSettings));
    }

    private static void AddSettingsInstance<TSettings>(this WebApplicationBuilder builder, string settingsName) where TSettings : class
    {
        builder.Services.Configure<TSettings>(builder.Configuration.GetSection(settingsName));
    }
}