using System;
using System.CommandLine.Parsing;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TeamsTranscript.Cli.Infrastructure;

namespace TeamsTranscript.Cli
{
    public static class Program
    {
        private static readonly ServiceCollection ServiceCollection = new();

        public static async Task<int> Main(string[] args)
        {
            ICompositeConsole console = new CompositeConsole();
            Console.OutputEncoding = Encoding.UTF8;

            return await new CommandLineParser(
                console,
                ServiceCollection).Create().InvokeAsync(args, console).ConfigureAwait(false);
        }
    }
}