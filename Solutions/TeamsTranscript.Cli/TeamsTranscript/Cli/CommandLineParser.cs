// <copyright file="CommandLineParser.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TeamsTranscript.Cli.Infrastructure;
using TeamsTranscript.Cli.Models;

namespace TeamsTranscript.Cli
{
    public class CommandLineParser
    {
        private readonly ICompositeConsole console;
        private readonly IServiceCollection services;

        public CommandLineParser(ICompositeConsole console, IServiceCollection services)
        {
            this.console = console;
            this.services = services;
        }

        public delegate Task ProcessReadable(IServiceCollection services, ProcessOptions options, ICompositeConsole console, InvocationContext invocationContext = null);

        public Parser Create(ProcessReadable processReadable = null)
        {
            // if environmentInit hasn't been provided (for testing) then assign the Command Handler
            processReadable ??= ProcessReadableHandler.ExecuteAsync;

            // Set up intrinsic commands that will always be available.
            RootCommand rootCommand = Root();
            rootCommand.AddCommand(Model());

            var commandBuilder = new CommandLineBuilder(rootCommand);

            return commandBuilder.UseDefaults().Build();

            static RootCommand Root()
            {
                return new RootCommand
                {
                    Name = "teams-transcript-cli",
                    Description = "A CLI Tool for Manipulating Microsoft Teams Transcripts.",
                };
            }

            Command Model()
            {
                var command = new Command(
                    "process",
                    "Process Teams Transcription Documents.");

                var readableCommand = new Command("readable", "Processes the transcript to merge transcription entries into blocks grouped by speaker for ease of reading.")
                {
                    Handler = CommandHandler.Create<ProcessOptions, InvocationContext>(async (options, context) =>
                    {
                        await processReadable(this.services, options, this.console, context).ConfigureAwait(false);
                    }),
                };

                readableCommand.AddArgument(new Argument<FileInfo>
                {
                    Name = "transcription-file-path",
                    Description = "Path to the transcript document.",
                    Arity = ArgumentArity.ExactlyOne,
                }.ExistingOnly());

                readableCommand.AddArgument(new Argument<FileInfo>
                {
                    Name = "output-file-path",
                    Description = "Path to the output file.",
                    Arity = ArgumentArity.ExactlyOne,
                });

                command.AddCommand(readableCommand);

                return command;
            }
        }
    }
}