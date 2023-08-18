using TeamsTranscript.Abstractions;
using TeamsTranscript.Abstractions.Configuration;
using TeamsTranscript.Abstractions.Transformers;
using TechTalk.SpecFlow;

namespace TeamsTranscript.Specs.Steps;

[Binding]
public class TranscriptionTransformerSteps
{
    private readonly ScenarioContext scenarioContext;

    public TranscriptionTransformerSteps(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [Given(@"I have the following list of transformations:")]
    public void GivenIHaveTheFollowingListOfTransformations(Table table)
    {
        List<Replacement> replacements = new();

        foreach (var row in table.Rows)
        {
            replacements.Add(new Replacement(row["old"], row["new"]));
        }

        this.scenarioContext.Add("transformation_options", new TransformationOptions(replacements));
    }

    [When(@"I transform the Transcription")]
    public void WhenITransformTheTranscription()
    {
        var transcriptions = this.scenarioContext.Get<IEnumerable<Transcription>>("transcriptions");
        var transformationOptions = this.scenarioContext.Get<TransformationOptions>("transformation_options");

        TranscriptionTransformer transformer = new();
        IEnumerable<Transcription> processesTranscriptions = transformer.Transform(transcriptions, transformationOptions);

        this.scenarioContext.Add("results", processesTranscriptions);
    }
}