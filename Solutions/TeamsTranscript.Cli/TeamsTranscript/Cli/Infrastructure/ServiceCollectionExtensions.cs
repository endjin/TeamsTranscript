// <copyright file="ServiceCollectionExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeamsTranscript.Cli.TeamsTranscript.Cli.Parsers;
using TeamsTranscriptOpenXmlDocumentReader = TeamsTranscript.Cli.TeamsTranscript.Cli.Parsers.TeamsTranscriptOpenXmlDocumentReader;

namespace TeamsTranscript.Cli.TeamsTranscript.Cli.Infrastructure;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures the service collection with the required dependencies for the command line application.
    /// </summary>
    /// <param name="serviceCollection">The service collection to add to.</param>
    public static void ConfigureDependencies(this ServiceCollection serviceCollection)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .Build();

        serviceCollection.AddSingleton<IConfiguration>(config);
        serviceCollection.AddCliServices(config);
    }

    /// <summary>
    /// Adds services required by the command line application to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add to.</param>
    /// <param name="config">The <see cref="IConfiguration"/>.</param>
    /// <returns>The service collection, for chaining.</returns>
    private static IServiceCollection AddCliServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddLogging(config => config.AddConsole());
        services.AddTranscriptionServices();

        return services;
    }

    private static void AddTranscriptionServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ITeamsTranscriptDocumentReader, TeamsTranscriptOpenXmlDocumentReader>();
        serviceCollection.AddTransient<ITranscriptionProcessor, TranscriptionProcessor>();
    }
}