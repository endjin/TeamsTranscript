# Teams Transcript

A CLI Tool for manipulating Microsoft Teams Call Transcripts. It can be used to process transcripts to make them more readable by humans, or convert into formats that are easier to process for use with LLMs.

## Installation

You can either use download the [TeamsTranscript.Abstractions](https://www.nuget.org/packages/TeamsTranscript.Abstractions) nuget package if you want to incorporate the parsing logic in your own application or Polyglot Notebook, or you can install the [teams-transcript](https://www.nuget.org/packages/teams-transcript) .NET Global Tool via the command line:

```bash
dotnet tool install --global teams-transcript
```

## Usage

Commands:

`process readable`

This converts the standard .docx transcript into a .txt or .json file where all contiguous transcription entries by the same speaker are merged into a single transcription block for ease of reading or processing.

```
.\teams-transcript.exe process readable -t transcript.docx -o transcript.txt
.\teams-transcript.exe process readable -t transcript.docx -o transcript.txt -f text
.\teams-transcript.exe process readable -t transcript.docx -o transcript.json -f json
.\teams-transcript.exe process readable --transcript-path transcript.docx --output-path transcript.txt 
.\teams-transcript.exe process readable --transcript-path transcript.docx --output-path transcript.txt --format text
.\teams-transcript.exe process readable --transcript-path transcript.docx --output-path transcript.json--format json
```

Given the following transcript (in a .docx):

```
0:0:0.0 --> 0:0:1.250
Jane Doe
Hi I'm Jane Doe, CEO
0:0:2.90 --> 0:0:4.480
John Doe
Hi, I'm John Doe, no relation, Ha! COO.
0:0:3.520 --> 0:0:5.460
Jane Doe
Today I want to discuss the plans for the next financial year.
0:0:5.300 --> 0:0:5.910
Jane Doe
This year has been turbulent, next year is predicted to be too.
0:0:7.80 --> 0:0:8.180
John Doe
And the turbulence hasn't been restricted to a single region.
0:0:8.810 --> 0:0:9.500
John Doe
It's been a global trend.
0:0:10.690 --> 0:0:11.510
Jane Doe
And that's what's worrisome, and why we need a plan.
```

`process readable -t transcript.docx -o transcript.txt -f text` produces:

```
0.0:0:0.000 --> 0.0:0:1.250
Jane Doe
Hi! I'm Jane Doe, CEO.
0.0:0:2.900 --> 0.0:0:4.480
John Doe
Hi, I'm John Doe, no relation, Ha! COO.
0.0:0:3.520 --> 0.0:0:5.910
Jane Doe
Today I want to discuss the plans for the next financial year. This year has been turbulent, next year is predicted to be too.
0.0:0:7.800 --> 0.0:0:9.500
John Doe
And the turbulence hasn't been restricted to a single region. It's been a global trend
0.0:0:10.690 --> 0.0:0:11.510
Jane Doe
And that's what's worrisome, and why we need a plan.
```

`process readable -t transcript.docx -o transcript.json -f json` produces:

```json
[
  {
    "Start": "00:00:00",
    "End": "00:00:01.2500000",
    "Speaker": "Jane Doe",
    "Script": "Hi! I\u0027m Jane Doe, CEO."
  },
  {
    "Start": "00:00:02.9000000",
    "End": "00:00:04.4800000",
    "Speaker": "John Doe",
    "Script": "Hi, I\u0027m John Doe, no relation, Ha! COO."
  },
  {
    "Start": "00:00:03.5200000",
    "End": "00:00:05.9100000",
    "Speaker": "Jane Doe",
    "Script": "Today I want to discuss the plans for the next financial year. This year has been turbulent, next year is predicted to be too."
  },
  {
    "Start": "00:00:07.8000000",
    "End": "00:00:09.5000000",
    "Speaker": "John Doe",
    "Script": "And the turbulence hasn\u0027t been restricted to a single region. It\u0027s been a global trend"
  },
  {
    "Start": "00:00:10.6900000",
    "End": "00:00:11.5100000",
    "Speaker": "Jane Doe",
    "Script": "And that\u0027s what\u0027s worrisome, and why we need a plan."
  }
]
```

## Licenses

[![GitHub license](https://img.shields.io/badge/License-Apache%202-blue.svg)](https://raw.githubusercontent.com/endjin/dotnet-adr/master/LICENSE)

This project is available under the Apache 2.0 open source license.

For any licensing questions, please email [&#108;&#105;&#99;&#101;&#110;&#115;&#105;&#110;&#103;&#64;&#101;&#110;&#100;&#106;&#105;&#110;&#46;&#99;&#111;&#109;](&#109;&#97;&#105;&#108;&#116;&#111;&#58;&#108;&#105;&#99;&#101;&#110;&#115;&#105;&#110;&#103;&#64;&#101;&#110;&#100;&#106;&#105;&#110;&#46;&#99;&#111;&#109;)

## Project Sponsor

This project is sponsored by [endjin](https://endjin.com), a UK based Technology Consultancy which specializes in Data, AI, DevOps & Cloud, and is a [.NET Foundation Corporate Sponsor](https://dotnetfoundation.org/membership/corporate-sponsorship).

> We help small teams achieve big things.

We produce two free weekly newsletters: 

 - [Azure Weekly](https://azureweekly.info) for all things about the Microsoft Azure Platform
 - [Power BI Weekly](https://powerbiweekly.info) for all things Power BI, Microsoft Fabric, and Azure Synapse Analytics

Keep up with everything that's going on at endjin via our [blog](https://endjin.com/blog), follow us on [Twitter](https://twitter.com/endjin), [YouTube](https://www.youtube.com/c/endjin) or [LinkedIn](https://www.linkedin.com/company/endjin).

We have become the maintainers of a number of popular .NET Open Source Projects:

- [Reactive Extensions for .NET](https://github.com/dotnet/reactive)
- [Reaqtor](https://github.com/reaqtive)
- [Argotic Syndication Framework](https://github.com/argotic-syndication-framework/)

And we have over 50 Open Source projects of our own, spread across the following GitHub Orgs:

- [endjin](https://github.com/endjin/)
- [Corvus](https://github.com/corvus-dotnet)
- [Menes](https://github.com/menes-dotnet)
- [Marain](https://github.com/marain-dotnet)
- [AIS.NET](https://github.com/ais-dotnet)

And the DevOps tooling we have created for managing all these projects is available on the [PowerShell Gallery](https://www.powershellgallery.com/profiles/endjin).

For more information about our products and services, or for commercial support of this project, please [contact us](https://endjin.com/contact-us). 