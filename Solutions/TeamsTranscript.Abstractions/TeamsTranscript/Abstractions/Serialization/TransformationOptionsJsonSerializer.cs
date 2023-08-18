using System.Text.Json;
using TeamsTranscript.Abstractions.Configuration;

namespace TeamsTranscript.Abstractions.Serialization;

public class TransformationOptionsJsonSerializer : ITransformationOptionsSerializer
{
    public TransformationOptions Deserialize(string content)
    {
        return JsonSerializer.Deserialize<TransformationOptions>(content)!;
    }
}