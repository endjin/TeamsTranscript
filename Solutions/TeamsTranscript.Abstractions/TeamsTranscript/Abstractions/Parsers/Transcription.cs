﻿// <copyright file="Transcription.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace TeamsTranscript.Abstractions.Parsers;

public record Transcription(TimeSpan Start, TimeSpan End, string Speaker, string Script);