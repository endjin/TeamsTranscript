// <copyright file="Transcription.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System;

namespace TeamsTranscript.Cli.Parsers
{
    public record Transcription(TimeSpan Start, TimeSpan End, string Speaker, string Script); 
}
