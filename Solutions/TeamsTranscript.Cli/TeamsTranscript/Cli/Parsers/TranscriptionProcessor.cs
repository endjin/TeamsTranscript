// <copyright file="TranscriptionProcessor.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace TeamsTranscript.Cli.Parsers
{
    public class TranscriptionProcessor : ITranscriptionProcessor
    {
        public IEnumerable<Transcription> Aggregate(IEnumerable<Transcription> transcripts) 
        {
            Transcription previous = null;
            Transcription output = null;

            foreach (var current in transcripts)
            {
                if (previous == null || current.Speaker != previous.Speaker)
                {
                    if (output != null)
                    {
                        yield return output;
                    }

                    output = current;
                }
                else
                {
                    output = new Transcription(output.Start, current.End, output.Speaker, output.Script + current.Script);
                }

                previous = current;
            }

            yield return output;
        }
    }
}