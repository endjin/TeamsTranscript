// <copyright file="TeamsTranscriptOpenXmlDocumentReader.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System.Text;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

namespace TeamsTranscript.Cli.Parsers;

public partial class TeamsTranscriptOpenXmlDocumentReader : ITeamsTranscriptDocumentReader
{
    public IEnumerable<Transcription> Read(string path)
    {
        using var doc = WordprocessingDocument.Open(path, false);
        StringBuilder sb = new();
        IEnumerable<OpenXmlElement>? elements = doc.MainDocumentPart?.Document?.Body?.Elements();

        if (elements is not null)
        {
            foreach (OpenXmlElement el in elements)
            {
                foreach (OpenXmlElement tr in el)
                {
                    foreach (OpenXmlElement run in tr)
                    {
                        if (!string.IsNullOrEmpty(run.InnerText))
                        {
                            sb.AppendLine(run.InnerText);
                        }
                    }
                }
            }
        }
        else
        {
            yield break;
        }
        
        Regex regex = TranscriptionEntryRegex();

        foreach (Match match in regex.Matches(sb.ToString()))
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