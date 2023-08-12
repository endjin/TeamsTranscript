// <copyright file="ITeamsTranscriptDocumentReader.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace TeamsTranscript.Cli.Parsers;

public interface ITeamsTranscriptDocumentReader
{
    IEnumerable<Transcription> Read(string path);
}