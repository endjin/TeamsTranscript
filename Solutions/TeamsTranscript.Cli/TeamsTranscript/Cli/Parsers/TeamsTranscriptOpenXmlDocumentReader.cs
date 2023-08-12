// <copyright file="TeamsTranscriptOpenXmlDocumentReader.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;

namespace TeamsTranscript.Cli.Parsers
{
    public class TeamsTranscriptOpenXmlDocumentReader : ITeamsTranscriptDocumentReader
    {
        public IEnumerable<Transcription> Read(string path)
        {
            using (var doc = WordprocessingDocument.Open(path, false))
            {
                var sb = new StringBuilder();

                foreach (var el in doc.MainDocumentPart.Document.Body.Elements())
                {
                    foreach (var tr in el)
                    {
                        foreach (var run in tr)
                        {
                            if (!string.IsNullOrEmpty(run.InnerText))
                            {
                                sb.AppendLine(run.InnerText);
                            }
                        }
                    }
                }

                Regex regex = new(@"(?<timestamp1>(\d{1,3}(:?|.?)){3}(\d{1,3}))\s-->\s(?<timestamp2>(\d{1,3}(:?|.?)){3}(\d{1,3}))\r?\n(?<speaker>.*)\r?\n(?<script>.*)");

                foreach (Match match in regex.Matches(sb.ToString()))
                {
                    yield return new Transcription(
                        TimeSpan.Parse(match.Groups["timestamp1"].Value),
                        TimeSpan.Parse(match.Groups["timestamp2"].Value),
                        match.Groups["speaker"].Value,
                        match.Groups["script"].Value);
                }
            }
        }
    }
}