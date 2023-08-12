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
}