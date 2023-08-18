namespace TeamsTranscript.Abstractions.Configuration;

public record TransformationOptions(List<Replacement> SpeakerReplacements);

public record Replacement(string Old, string New);