namespace TeamsTranscript.Abstractions.Configuration;

public record ConfigurationOptions(List<Replacement> SpeakerReplacements);

public record Replacement(string Old, string New);