using Shouldly;
using TeamsTranscript.Abstractions;
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

    [When(@"I ask for transcriptions grouped by (.*)")]
    public void WhenlAskForTranscriptionsGroupedBy(TimeSpan timeSpan)
    {
        var transcriptions = this.scenarioContext.Get<IEnumerable<Transcription>>("transcriptions");
        IEnumerable<IEnumerable<Transcription>> result = transcriptions.Window(timeSpan);
        this.scenarioContext.Add("grouped_results", result);
    }


    [Then("I should get the following grouped transcriptions:")]
    public void ThenIShouldGetTheFollowingGroupedTranscriptions(Table table)
    {
        Dictionary<int, List<Transcription>> groups = new();

        foreach (var row in table.Rows)
        {
            var index = int.Parse(row["list"]);
            var start = TimeSpan.Parse(row["start"]);
            var end = TimeSpan.Parse(row["end"]);
            var speaker = row["speaker"];
            var script = row["script"];

            var transcription = new Transcription(start, end, speaker, script);

            if (groups.ContainsKey(index))
            {
                groups[index].Add(transcription);
            }
            else
            {
                groups.Add(index, new List<Transcription> { transcription });
            }
        }

        List<IEnumerable<Transcription>> results = this.scenarioContext.Get<IEnumerable<IEnumerable<Transcription>>>("grouped_results").ToList();

        results.Count().ShouldBe(groups.Count);

        foreach (KeyValuePair<int, List<Transcription>> group in groups)
        {
            var result = results.ElementAt(group.Key);
            result.Count().ShouldBe(group.Value.Count);

            for (int i = 0; i < result.Count(); i++)
            {
                Transcription transcription = result.ElementAt(i);

                string start = group.Value[i].Start.ToString();
                string end = group.Value[i].End.ToString();
                string speaker = group.Value[i].Speaker;
                string script = group.Value[i].Script;

                transcription.Start.ShouldBe(TimeSpan.Parse(start));
                transcription.End.ShouldBe(TimeSpan.Parse(end));
                transcription.Speaker.ShouldBe(speaker);
                transcription.Script.ShouldBe(script);
            }
        }
    }
}