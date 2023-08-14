// <copyright file="ITranscriptionParser.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace TeamsTranscript.Abstractions.Parsers;

public interface ITranscriptionParser
{
    IEnumerable<Transcription> Parse(string content);
}