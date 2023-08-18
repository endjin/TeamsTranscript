using TeamsTranscript.Abstractions.Configuration;

namespace TeamsTranscript.Abstractions.Persistence;

public interface IConfigurationOptionsPersistence
{
    Task<ConfigurationOptions> RetrieveAsync(FileInfo inputFilePath);
}