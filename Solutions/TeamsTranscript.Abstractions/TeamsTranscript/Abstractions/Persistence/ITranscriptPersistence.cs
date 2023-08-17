namespace TeamsTranscript.Abstractions.Persistence;

public interface ITranscriptPersistence
{
    Task PersistAsync(IEnumerable<Transcription> transcripts, FileInfo outputFilePath);

    Task<IEnumerable<Transcription>> RetrieveAsync(FileInfo inputFilePath);
}