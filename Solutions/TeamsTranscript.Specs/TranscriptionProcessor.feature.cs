﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace TeamsTranscript.Specs
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("TranscriptionProcessor")]
    public partial class TranscriptionProcessorFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "TranscriptionProcessor.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "TranscriptionProcessor", @"I want to be able to process a list of transcription data structures
And group contiguous conversation blocks by the same speaker into a single transcription entry
The timespan should be updated to include the aggregate window, and the script should be concatenated. ", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Process a list of transcription entries")]
        [NUnit.Framework.CategoryAttribute("tag1")]
        public void ProcessAListOfTranscriptionEntries()
        {
            string[] tagsOfScenario = new string[] {
                    "tag1"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Process a list of transcription entries", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 8
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "start",
                            "end",
                            "speaker",
                            "script"});
                table2.AddRow(new string[] {
                            "0:0:0.0",
                            "0:0:1.250",
                            "Jane Doe",
                            "Hi I\'m Jane Doe, CEO."});
                table2.AddRow(new string[] {
                            "0:0:2.90",
                            "0:0:4.480",
                            "John Doe",
                            "Hi, I\'m John Doe, no relation, Ha! COO."});
                table2.AddRow(new string[] {
                            "0:0:3.520",
                            "0:0:5.460",
                            "Jane Doe",
                            "Today I want to discuss the plans for the next financial year."});
                table2.AddRow(new string[] {
                            "0:0:5.300",
                            "0:0:5.910",
                            "Jane Doe",
                            "This year has been turbulent, next year is predicted to be too."});
                table2.AddRow(new string[] {
                            "0:0:7.80",
                            "0:0:8.180",
                            "John Doe",
                            "And the turbulence hasn\'t been restricted to a single region."});
                table2.AddRow(new string[] {
                            "0:0:8.810",
                            "0:0:9.500",
                            "John Doe",
                            "It\'s been a global trend"});
                table2.AddRow(new string[] {
                            "0:0:10.690",
                            "0:0:11.510",
                            "Jane Doe",
                            "And that\'s what\'s worrisome, and why we need a plan."});
#line 9
 testRunner.Given("I have the following transcription entries:", ((string)(null)), table2, "Given ");
#line hidden
#line 18
 testRunner.When("I process the Transcription", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "start",
                            "end",
                            "speaker",
                            "script"});
                table3.AddRow(new string[] {
                            "0:0:0.0",
                            "0:0:1.250",
                            "Jane Doe",
                            "Hi I\'m Jane Doe, CEO."});
                table3.AddRow(new string[] {
                            "0:0:2.90",
                            "0:0:4.480",
                            "John Doe",
                            "Hi, I\'m John Doe, no relation, Ha! COO."});
                table3.AddRow(new string[] {
                            "0:0:3.520",
                            "0:0:5.910",
                            "Jane Doe",
                            "Today I want to discuss the plans for the next financial year. This year has been" +
                                " turbulent, next year is predicted to be too."});
                table3.AddRow(new string[] {
                            "0:0:7.80",
                            "0:0:9.500",
                            "John Doe",
                            "And the turbulence hasn\'t been restricted to a single region. It\'s been a global " +
                                "trend"});
                table3.AddRow(new string[] {
                            "0:0:10.690",
                            "0:0:11.510",
                            "Jane Doe",
                            "And that\'s what\'s worrisome, and why we need a plan."});
#line 19
 testRunner.Then("I should get a list of aggregated Transcription data structures with the followin" +
                        "g content:", ((string)(null)), table3, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
