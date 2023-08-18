using TeamsTranscript.Abstractions.Configuration;

namespace TeamsTranscript.Abstractions.Persistence;

public interface ITransformationOptionsPersistence
{
    Task<TransformationOptions> RetrieveAsync(FileInfo inputFilePath);
}