// <copyright file="ProcessReadableCommand.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

using Spectre.Console.Cli;
using TeamsTranscript.Abstractions;
using TeamsTranscript.Abstractions.Parsers;
using TeamsTranscript.Abstractions.Persistence;

namespace TeamsTranscript.Cli.Commands;

/// <summary>
/// A command to set the enrichment configuration for a feed collection.
/// </summary>
public class ProcessReadableCommand : AsyncCommand<ProcessReadableCommand.Settings>
{
    private readonly IServiceProvider serviceProvider;
    private readonly ITranscriptionParser parser;
    private readonly ITranscriptionProcessor processor;
    private readonly ITeamsTranscriptDocumentReader reader;

    public ProcessReadableCommand(IServiceProvider serviceProvider, ITeamsTranscriptDocumentReader reader, ITranscriptionProcessor processor, ITranscriptionParser parser)
    {
        this.serviceProvider = serviceProvider;
        this.reader = reader;
        this.processor = processor;
        this.parser = parser;
    }

    /// <inheritdoc/>
    public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        await using Stream stream = File.OpenRead(settings.TranscriptionFilePath.FullName);
        string content = reader.Read(stream);
        IEnumerable<Transcription> transcripts = parser.Parse(content);
        IEnumerable<Transcription> merged = processor.Aggregate(transcripts);

        settings.OutputFormat ??= TranscriptFormat.Text;

        ITranscriptPersistence transcriptPersistence = this.serviceProvider.GetContent<ITranscriptPersistence>(settings.OutputFormat.ToString());

        await transcriptPersistence.PersistAsync(merged, settings.OutputFilePath).ConfigureAwait(false);
       
        return ReturnCodes.Ok;
    }

    /// <summary>
    /// The settings for the command.
    /// </summary>
    public class Settings : CommandSettings
    {
#nullable disable annotations
        /// <summary>
        /// Gets or sets the path to the Teams transcription file.
        /// </summary>
        [CommandOption("-t|--transcript-path <TranscriptionFilePath>")]
        [Description("Teams Transcription File Path")]
        public FileInfo TranscriptionFilePath { get; init; }

        /// <summary>
        /// Gets or sets the Feed Collection Id to set the configuration for.
        /// </summary>
        [CommandOption("-o|--output-path <OutputFilePath>")]
        [Description("Output File Path")]
        public FileInfo OutputFilePath { get; init; }

        [CommandOption("-f|--format <TranscriptFormat>")]
        [Description("Output Format")]
        public TranscriptFormat? OutputFormat { get; set; }
#nullable enable annotations
    }
}