namespace TeamsTranscript.Abstractions;

public static class TranscriptionsExtensions
{
    public static IEnumerable<Transcription> FilterByTimeSpan(this IEnumerable<Transcription> transcriptions, TimeSpan start, TimeSpan end)
    {
        var orderedTranscriptions = transcriptions.OrderBy(t => t.Start).ToList();

        return orderedTranscriptions.Where(t => t.Start >= start && t.End <= end);
    }

    public static IEnumerable<string> Participants(this IEnumerable<Transcription> transcriptions)
    {
        return transcriptions.Select(t => t.Speaker).Distinct(StringComparer.InvariantCultureIgnoreCase).Order();
    }

    public static string ParticipantsAsCommaDelimitedString(this IEnumerable<Transcription> transcriptions)
    {
        return string.Join(", ", transcriptions.Participants());
    }

    public static IEnumerable<IEnumerable<Transcription>> Window(this IEnumerable<Transcription> transcriptions, TimeSpan timeSpan)
    {
        TimeSpan lastTimeSpan = TimeSpan.Zero;

        var list = new List<Transcription>();

        foreach (Transcription transcription in transcriptions.OrderBy(t => t.Start))
        {
            if (transcription.Start - lastTimeSpan >= timeSpan)
            {
                if (list.Count > 0)
                {
                    yield return list;
                    list = new List<Transcription>();
                }

                lastTimeSpan = transcription.Start;
            }

            list.Add(transcription);
        }

        yield return list;
    }
}