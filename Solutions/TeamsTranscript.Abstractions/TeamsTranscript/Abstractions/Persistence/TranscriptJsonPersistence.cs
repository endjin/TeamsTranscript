using System.Text.Json;

namespace TeamsTranscript.Abstractions.Persistence;

public class TranscriptJsonPersistence : ITranscriptPersistence
{
    public async Task PersistAsync(IEnumerable<Transcription> transcripts, FileInfo outputFilePath)
    {
        if (outputFilePath?.Directory is { Exists: false })
        {
            outputFilePath.Directory.Create();
        }

        await File.WriteAllTextAsync(Path.ChangeExtension(outputFilePath!.FullName, ".json"), JsonSerializer.Serialize(transcripts, new JsonSerializerOptions { WriteIndented = true })).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Transcription>> RetrieveAsync(FileInfo inputFilePath)
    {
        if (inputFilePath is null)
        {
            throw new ArgumentNullException(nameof(inputFilePath));
        }

        if (!inputFilePath.Exists)
        {
            throw new FileNotFoundException($"The file {inputFilePath.FullName} does not exist.", inputFilePath.FullName);
        }

        return JsonSerializer.Deserialize<IEnumerable<Transcription>>(await File.ReadAllTextAsync(inputFilePath.FullName).ConfigureAwait(false))!;
    }
}