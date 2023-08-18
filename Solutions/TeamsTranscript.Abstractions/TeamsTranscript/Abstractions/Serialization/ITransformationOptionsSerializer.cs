using TeamsTranscript.Abstractions.Configuration;

namespace TeamsTranscript.Abstractions.Serialization;

public interface ITransformationOptionsSerializer
{
    TransformationOptions Deserialize(string content);
}