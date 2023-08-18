using TeamsTranscript.Abstractions.Configuration;
using TeamsTranscript.Abstractions.Serialization;

namespace TeamsTranscript.Abstractions.Persistence;

public class TransformationOptionsFileSystemPersistence : ITransformationOptionsPersistence
{
    private readonly ITransformationOptionsSerializer transformationOptionsSerializer;

    public TransformationOptionsFileSystemPersistence(ITransformationOptionsSerializer transformationOptionsSerializer)
    {
        this.transformationOptionsSerializer = transformationOptionsSerializer;
    }

    public async Task<TransformationOptions> RetrieveAsync(FileInfo inputFilePath)
    {
        if (inputFilePath is null)
        {
            throw new ArgumentNullException(nameof(inputFilePath));
        }

        if (!inputFilePath.Exists)
        {
            throw new FileNotFoundException($"The file {inputFilePath.FullName} does not exist.", inputFilePath.FullName);
        }

        return this.transformationOptionsSerializer.Deserialize(await File.ReadAllTextAsync(inputFilePath.FullName).ConfigureAwait(false));
    }
}