using System.Text;
using TeamsTranscript.Abstractions.Parsers;

namespace TeamsTranscript.Abstractions.Persistence;

public class TranscriptTextPersistence : ITranscriptPersistence
{
    private readonly ITranscriptionParser parser;

    public TranscriptTextPersistence(ITranscriptionParser parser)
    {
        this.parser = parser;
    }

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
        
        return this.parser.Parse(await File.ReadAllTextAsync(inputFilePath.FullName).ConfigureAwait(false)); ;
    }
}