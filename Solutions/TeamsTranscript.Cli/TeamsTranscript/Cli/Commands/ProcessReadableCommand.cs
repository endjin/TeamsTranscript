// <copyright file="ProcessReadableCommand.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using Spectre.Console.Cli;
using TeamsTranscript.Cli.Abstractions;
using TeamsTranscript.Cli.Parsers;

namespace TeamsTranscript.Cli.Commands;

/// <summary>
/// A command to set the enrichment configuration for a feed collection.
/// </summary>
public class ProcessReadableCommand : AsyncCommand<ProcessReadableCommand.Settings>
{
    private readonly ITranscriptionProcessor processor;
    private readonly ITeamsTranscriptDocumentReader reader;

    public ProcessReadableCommand(ITeamsTranscriptDocumentReader reader, ITranscriptionProcessor processor)
    {
        this.reader = reader;
        this.processor = processor;
    }

    /// <inheritdoc/>
    public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        IEnumerable<Transcription> transcripts = reader.Read(settings.TranscriptionFilePath.FullName);
        IEnumerable<Transcription> merged = processor.Aggregate(transcripts);

        if (settings.OutputFilePath?.Directory is { Exists: false })
        {
            settings.OutputFilePath.Directory.Create();
        }

        switch (settings.OutputFormat)
        {
            case OutputFormat.Text:
            {
                await PersistAsTextAsync(settings, merged).ConfigureAwait(false);
                break;
            }
            case OutputFormat.Json:
                await PersistAsJsonAsync(settings, merged).ConfigureAwait(false);
                break;
            default: goto case OutputFormat.Text;
        }           
        
        return ReturnCodes.Ok;
    }

    private static async Task PersistAsJsonAsync(Settings settings, IEnumerable<Transcription> merged)
    {
        await File.WriteAllTextAsync(Path.ChangeExtension(settings.OutputFilePath!.FullName, ".json") , JsonSerializer.Serialize(merged, new JsonSerializerOptions { WriteIndented = true })).ConfigureAwait(false);
    }

    private static async Task PersistAsTextAsync(Settings settings, IEnumerable<Transcription> merged)
    {
        StringBuilder sb = new();

        foreach (var transcription in merged)
        {
            sb.AppendLine($"{transcription.Start.ToString(@"d\.h\:m\:s\.fff")} --> {transcription.End.ToString(@"d\.h\:m\:s\.fff")}");
            sb.AppendLine(transcription.Speaker);
            sb.AppendLine(transcription.Script);
        }

        await File.WriteAllTextAsync(settings.OutputFilePath!.FullName, sb.ToString()).ConfigureAwait(false);
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

        [CommandOption("-f|--format <OutputFormat>")]
        [Description("Output Format")]
        public OutputFormat? OutputFormat { get; init; }
#nullable enable annotations
    }

    public enum OutputFormat
    {
        Text = 0,
        Json = 1,
    }
}