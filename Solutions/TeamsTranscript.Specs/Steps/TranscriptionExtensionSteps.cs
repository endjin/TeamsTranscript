using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Shouldly;
using TeamsTranscript.Abstractions;
using TeamsTranscript.Abstractions.Parsers;
using TechTalk.SpecFlow;

namespace TeamsTranscript.Specs.Steps;

[Binding]
public class TranscriptionExtensionSteps
{
    private readonly ScenarioContext scenarioContext;

    public TranscriptionExtensionSteps(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [When(@"I ask for a distinct list of participants")]
    public void WhenIAskForADistinctListOfParticipants()
    {
        var transcriptions = this.scenarioContext.Get<IEnumerable<Transcription>>("transcriptions");
        this.scenarioContext.Add("participants", transcriptions.Participants());
    }

    [Then(@"I should get a list of participants with the following content:")]
    public void ThenIShouldGetAListOfParticipantsWithTheFollowingContent(Table table)
    {
        var participants = this.scenarioContext.Get<IEnumerable<string>>("participants");

        participants.ShouldBe(table.Rows.Select(r => r["name"]));
    }

    [When(@"I ask for a comma separated list of participants")]
    public void WhenIAskForACommaSeparatedListOfParticipants()
    {
        var transcriptions = this.scenarioContext.Get<IEnumerable<Transcription>>("transcriptions");
        this.scenarioContext.Add("participants_comma_separated_string", transcriptions.ParticipantsAsCommaDelimitedString());
    }

    [Then(@"I should get a comma separated list of participants with the following content:")]
    public void ThenIShouldGetACommaSeparatedListOfParticipantsWithTheFollowingContent(Table table)
    {
        string participants = this.scenarioContext.Get<string>("participants_comma_separated_string");

        participants.ShouldBe(table.Rows.Select(r => r["list"]).First());
    }

    [When(@"I ask for transcriptions between (.*) and (.*)")]
    public void WhenIAskForTranscriptionsBetweenAnd(TimeSpan start, TimeSpan end)
    {
        var transcriptions = this.scenarioContext.Get<IEnumerable<Transcription>>("transcriptions");

        this.scenarioContext.Add("filtered_results", transcriptions.FilterByTimeSpan(start, end));
    }

    [Then(@"I should get the following transcriptions:")]
    public void ThenIShouldGetTheFollowingTranscriptions(Table table)
    {
        var results = this.scenarioContext.Get<IEnumerable<Transcription>>("filtered_results");

        results.Count().ShouldBe(table.Rows.Count);

        for (int i = 0; i < results.Count() - 1; i++)
        {
            Transcription transcription = results.ElementAt(i);

            string start = table.Rows[i]["start"];
            string end = table.Rows[i]["end"];
            string speaker = table.Rows[i]["speaker"];
            string script = table.Rows[i]["script"];

            transcription.Start.ShouldBe(TimeSpan.Parse(start));
            transcription.End.ShouldBe(TimeSpan.Parse(end));
            transcription.Speaker.ShouldBe(speaker);
            transcription.Script.ShouldBe(script);
        }
    }
}