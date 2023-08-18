using TeamsTranscript.Abstractions.Configuration;

namespace TeamsTranscript.Abstractions.Transformers;

public class TranscriptionTransformer : ITranscriptionTransformer
{
    public IEnumerable<Transcription> Transform(IEnumerable<Transcription> transcriptions, TransformationOptions transformationOptions)
    {
        // use the transformation options to transform the transcription speakers, dealing with cases like Doe, John, should be John Doe

        foreach (var transcription in transcriptions)
        {
            yield return transcription with 
            { 
                Speaker = transformationOptions.SpeakerReplacements.Aggregate(
                    transcription.Speaker, 
                    (current, replacement) => 
                        current.Replace(replacement.Old, replacement.New))
            };
        }
    }
}