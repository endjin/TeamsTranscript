// <copyright file="RegExpTranscriptionParser.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System.Text.RegularExpressions;

namespace TeamsTranscript.Abstractions.Parsers;

public partial class RegExpTranscriptionParser : ITranscriptionParser
{
    public IEnumerable<Transcription> Parse(string content)
    {
        Regex regex = TranscriptionEntryRegex();

        foreach (Match match in regex.Matches(content))
        {
            yield return new Transcription(
                TimeSpan.Parse(match.Groups["timestamp1"].Value),
                TimeSpan.Parse(match.Groups["timestamp2"].Value),
                match.Groups["speaker"].Value,
                match.Groups["script"].Value);
        }
    }

    [GeneratedRegex("(?<timestamp1>(\\d{1,3}(:?|.?)){3}(\\d{1,3}))\\s-->\\s(?<timestamp2>(\\d{1,3}(:?|.?)){3}(\\d{1,3}))\\r?\\n(?<speaker>.*)\\r\\n(?<script>.*)\\r\\n")]
    private static partial Regex TranscriptionEntryRegex();
}