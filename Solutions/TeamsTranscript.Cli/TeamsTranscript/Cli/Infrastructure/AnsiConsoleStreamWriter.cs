using System.CommandLine.IO;
using Spectre.Console;

namespace TeamsTranscript.Cli.Infrastructure
{
    internal sealed class AnsiConsoleStreamWriter : IStandardStreamWriter
    {
        private readonly IAnsiConsole console;

        public AnsiConsoleStreamWriter(IAnsiConsole console)
        {
            this.console = console;
        }

        public void Write(string value)
        {
            this.console.Write(value);
        }
    }
}