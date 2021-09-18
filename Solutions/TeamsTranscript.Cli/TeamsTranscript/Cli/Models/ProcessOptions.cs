// <copyright file="ListOptions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System.IO;

namespace TeamsTranscript.Cli.Models
{
    public class ProcessOptions
    {
        public FileInfo TranscriptionFilePath { get; }
        
        public FileInfo OutputFilePath { get; }

        public ProcessOptions(FileInfo transcriptionFilePath, FileInfo outputFilePath)
        {
            this.TranscriptionFilePath = transcriptionFilePath;
            this.OutputFilePath = outputFilePath;
        }
    }
} 