// <copyright file="ITeamsTranscriptDocumentReader.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace TeamsTranscript.Abstractions.Parsers;

public interface ITeamsTranscriptDocumentReader
{
    string Read(Stream content);
}