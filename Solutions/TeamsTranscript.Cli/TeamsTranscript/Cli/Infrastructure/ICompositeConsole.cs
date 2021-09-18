using System.CommandLine;
using Spectre.Console;

namespace TeamsTranscript.Cli.Infrastructure
{
    public interface ICompositeConsole : IConsole, IAnsiConsole
    {
    }
}