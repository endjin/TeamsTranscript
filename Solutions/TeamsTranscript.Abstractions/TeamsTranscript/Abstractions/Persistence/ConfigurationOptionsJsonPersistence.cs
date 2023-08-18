using System.Text.Json;
using TeamsTranscript.Abstractions.Configuration;

namespace TeamsTranscript.Abstractions.Persistence;

public class ConfigurationOptionsJsonPersistence : IConfigurationOptionsPersistence
{
    public async Task<ConfigurationOptions> RetrieveAsync(FileInfo inputFilePath)
    {
        if (inputFilePath is null)
        {
            throw new ArgumentNullException(nameof(inputFilePath));
        }

        if (!inputFilePath.Exists)
        {
            throw new FileNotFoundException($"The file {inputFilePath.FullName} does not exist.", inputFilePath.FullName);
        }

        return JsonSerializer.Deserialize<ConfigurationOptions>(await File.ReadAllTextAsync(inputFilePath.FullName).ConfigureAwait(false))!;
    }
}