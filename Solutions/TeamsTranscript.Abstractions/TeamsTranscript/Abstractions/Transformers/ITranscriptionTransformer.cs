using TeamsTranscript.Abstractions.Configuration;

namespace TeamsTranscript.Abstractions.Transformers;

public interface ITranscriptionTransformer
{
    IEnumerable<Transcription> Transform(IEnumerable<Transcription> transcriptions, TransformationOptions transformationOptions);
}