﻿// <copyright file="ServiceCollectionExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using Corvus.ContentHandling;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeamsTranscript.Abstractions;
using TeamsTranscript.Abstractions.Parsers;
using TeamsTranscript.Abstractions.Persistence;
using TeamsTranscript.Cli.Commands;

namespace TeamsTranscript.Cli.Infrastructure;

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
        serviceCollection.AddContent();
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
        serviceCollection.AddTransient<ITranscriptionParser, RegExpTranscriptionParser>();
        serviceCollection.AddTransient<ITranscriptionProcessor, TranscriptionProcessor>();
    }

    /// <summary>
    /// Registers the content management content types with the factory.
    /// </summary>
    /// <param name="factory">The factory with which to register the content.</param>
    /// <returns>The factory with the content registered.</returns>
    private static ContentFactory RegisterContent(this ContentFactory factory)
    {
        // Page Template Readers
        factory.RegisterTransientContent<TranscriptJsonPersistence>(TranscriptFormat.Json.ToString());
        factory.RegisterTransientContent<TranscriptTextPersistence>(TranscriptFormat.Text.ToString());

        return factory;
    }

    /// <summary>
    /// Add content management content to the container.
    /// </summary>
    /// <param name="serviceCollection">The service collection to which to add the content.</param>
    /// <returns>The service collection wth the content added.</returns>
    private static IServiceCollection AddContent(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddContent(factory => factory.RegisterContent());
    }
}