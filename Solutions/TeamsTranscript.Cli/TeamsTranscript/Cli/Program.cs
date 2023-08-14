using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using TeamsTranscript.Cli.Commands;
using TeamsTranscript.Cli.Infrastructure;
using TeamsTranscript.Cli.Infrastructure.Injection;

namespace TeamsTranscript.Cli;

public static class Program
{
    public static Task<int> Main(string[] args)
    {
        ServiceCollection registrations = new();
        registrations.ConfigureDependencies();

        TypeRegistrar registrar = new(registrations);
        CommandApp app = new(registrar);

        app.Configure(config =>
        {
            config.Settings.PropagateExceptions = false;
            config.CaseSensitivity(CaseSensitivity.None);
            config.SetApplicationName("teams-transcript");

            config.AddExample("process", "readable", "-t", "transcript.docx", "-o", "transcript.txt");
            config.AddExample("process", "readable", "-t", "transcript.docx", "-o", "transcript.txt", "-f", "text");
            config.AddExample("process", "readable", "-t", "transcript.docx", "-o", "transcript.txt", "-f", "json");
            config.AddExample("process", "readable", "--transcript-path", "transcript.docx", "--output-path", "transcript.txt");
            config.AddExample("process", "readable", "--transcript-path", "transcript.docx", "--output-path", "transcript.txt", "--format", "text");
            config.AddExample("process", "readable", "--transcript-path", "transcript.docx", "--output-path", "transcript.txt", "--format", "json");

            config.AddBranch("process", process =>
            {
                process.SetDescription("Operations to process the Teams Transcript.");
                process.AddCommand<ProcessReadableCommand>("readable")
                       .WithDescription("Converts the transcripts into a readable format by merging concurrent speaker blocks");
            });

            config.ValidateExamples();
        });

        return app.RunAsync(args);
    }
}