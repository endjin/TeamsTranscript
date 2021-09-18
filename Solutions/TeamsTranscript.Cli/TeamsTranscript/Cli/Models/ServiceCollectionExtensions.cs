// <copyright file="ServiceCollectionExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using TeamsTranscript.Cli.Parsers;

namespace TeamsTranscript.Cli.Models
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds application wide services.
        /// </summary>
        /// <param name="serviceCollection">Application's ServiceCollection.</param>
        public static void AddTranscriptionServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITeamsTranscriptDocumentReader, TeamsTranscriptOpenXmlDocumentReader>();
            serviceCollection.AddTransient<ITranscriptionProcessor, TranscriptionProcessor>();
        }
    }
}