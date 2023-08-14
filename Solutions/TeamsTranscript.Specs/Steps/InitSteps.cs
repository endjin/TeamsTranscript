using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Shouldly;
using TeamsTranscript.Abstractions;
using TeamsTranscript.Abstractions.Parsers;
using TechTalk.SpecFlow;

namespace TeamsTranscript.Specs.Steps;

[Binding]
public class InitSteps
{
    private readonly ScenarioContext scenarioContext;
    private readonly ITranscriptionParser parser;

    public InitSteps(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
        this.parser = new RegExpTranscriptionParser();
    }

    [Given(@"I have a transcription file with the following content")]
    public void GivenIHaveATranscriptionFileWithTheFollowingContent(string transcription)
    {
        this.scenarioContext.Add("transcription", transcription);
    }

    [When(@"I parse the Transcription")]
    public void WhenIParseTheTranscription()
    {
        IEnumerable<Transcription> transcriptions = this.parser.Parse(this.scenarioContext.Get<string>("transcription"));
        this.scenarioContext.Add("transcriptions", transcriptions);
    }

    [Then(@"I should get a list of Transcription data structures with the following content:")]
    public void ThenIShouldGetAListOfTranscriptionDataStructuresWithTheFollowingContent(Table table)
    {
        IEnumerable<Transcription> transcriptions = this.scenarioContext.Get<IEnumerable<Transcription>>("transcriptions");

        for (int i = 0; i < transcriptions.Count() -1; i++)
        {
            Transcription transcription = transcriptions.ElementAt(i);

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