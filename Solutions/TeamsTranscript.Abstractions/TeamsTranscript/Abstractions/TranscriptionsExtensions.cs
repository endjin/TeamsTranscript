using System.Reactive.Linq;

namespace TeamsTranscript.Abstractions;

public static class TranscriptionsExtensions
{
    public static IEnumerable<Transcription> FilterByTimeSpan(this IEnumerable<Transcription> transcriptions, TimeSpan start, TimeSpan end)
    {
        var orderedTranscriptions = transcriptions.OrderBy(t => t.Start).ToList();

        return orderedTranscriptions.Where(t => t.Start >= start && t.End <= end); ;
    }

    public static IEnumerable<string> Participants(this IEnumerable<Transcription> transcriptions)
    {
        return transcriptions.Select(t => t.Speaker).Distinct(StringComparer.InvariantCultureIgnoreCase).Order();
    }

    public static string ParticipantsAsCommaDelimitedString(this IEnumerable<Transcription> transcriptions)
    {
        return string.Join(", ", transcriptions.Participants());
    }

}