using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Shouldly;
using TeamsTranscript.Abstractions;
using TeamsTranscript.Abstractions.Parsers;
using TechTalk.SpecFlow;

namespace TeamsTranscript.Specs.Steps;

[Binding]
public class TranscriptionProcessorSteps
{
    private readonly ScenarioContext scenarioContext;
    private readonly ITranscriptionProcessor processor;

    public TranscriptionProcessorSteps(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
        this.processor = new TranscriptionProcessor();
    }

    [Given(@"I have the following transcription entries:")]
    public void GivenIHaveTheFollowingTranscriptionEntries(Table table)
    {
        List<Transcription> transcriptions = new();

        foreach (var row in table.Rows)
        {
            transcriptions.Add(new Transcription(
                               TimeSpan.Parse(row["start"]),
                                              TimeSpan.Parse(row["end"]),
                                              row["speaker"],
                                              row["script"]));
        }
        
        this.scenarioContext.Add("transcriptions", transcriptions);
    }

    [When(@"I process the Transcription")]
    public void WhenIProcessTheTranscription()
    {
        var transcriptions = this.scenarioContext.Get<IEnumerable<Transcription>>("transcriptions");
        IEnumerable<Transcription> results = this.processor.Aggregate(transcriptions);
        this.scenarioContext.Add("results", results);
    }

    [Then(@"I should get a list of aggregated Transcription data structures with the following content:")]
    public void ThenIShouldGetAListOfAggregatedTranscriptionDataStructuresWithTheFollowingContent(Table table)
    {
        List<Transcription>? transcriptions = this.scenarioContext.Get<IEnumerable<Transcription>>("results").ToList();

        for (int i = 0; i < transcriptions.Count() - 1; i++)
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