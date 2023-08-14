using System.Text;

namespace TeamsTranscript.Abstractions.Persistence;

public class TranscriptTextPersistence : ITranscriptPersistence
{
    public async Task PersistAsync(IEnumerable<Transcription> transcripts, FileInfo outputFilePath)
    {
        if (outputFilePath?.Directory is { Exists: false })
        {
            outputFilePath.Directory.Create();
        }

        StringBuilder sb = new();

        foreach (var transcription in transcripts)
        {
            sb.AppendLine($"{transcription.Start.ToString(@"d\.h\:m\:s\.fff")} --> {transcription.End.ToString(@"d\.h\:m\:s\.fff")}");
            sb.AppendLine(transcription.Speaker);
            sb.AppendLine(transcription.Script);
        }

        await File.WriteAllTextAsync(outputFilePath!.FullName, sb.ToString()).ConfigureAwait(false);
    }
}