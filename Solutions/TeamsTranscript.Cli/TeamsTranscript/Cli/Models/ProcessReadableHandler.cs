// <copyright file="ModelConvertHandler.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System.CommandLine.Invocation;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TeamsTranscript.Cli.Abstractions;
using TeamsTranscript.Cli.Infrastructure;
using TeamsTranscript.Cli.Parsers;

namespace TeamsTranscript.Cli.Models
{
    public static class ProcessReadableHandler
    {
        public static async Task<int> ExecuteAsync(
            IServiceCollection services,
            ProcessOptions processOptions,
            ICompositeConsole console,
            InvocationContext context = null)
        {
            services.AddTranscriptionServices();
            
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var reader = serviceProvider.GetService<ITeamsTranscriptDocumentReader>();
            var processor = serviceProvider.GetService<ITranscriptionProcessor>();

            var transcripts = reader.Read(processOptions.TranscriptionFilePath.FullName);
            var merged = processor.Aggregate(transcripts);

            var sb = new StringBuilder();

            foreach (var transcription in merged)
            {
                sb.AppendLine($"{transcription.Start} --> {transcription.End}");
                sb.AppendLine(transcription.Speaker);
                sb.AppendLine(transcription.Script);
            }
            
            await File.WriteAllTextAsync(processOptions.OutputFilePath.FullName, sb.ToString()).ConfigureAwait(false);

            return ReturnCodes.Ok;
        }
    }
}