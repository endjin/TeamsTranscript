Feature: TranscriptionProcessor

I want to be able to process a list of transcription data structures
And group contiguous conversation blocks by the same speaker into a single transcription entry
The timespan should be updated to include the aggregate window, and the script should be concatenated. 

@tag1
Scenario: Process a list of transcription entries
	Given I have the following transcription entries:
		| start      | end        | speaker  | script                                                          |
		| 0:0:0.0    | 0:0:1.250  | Jane Doe | Hi I'm Jane Doe, CEO.                                           |
		| 0:0:2.90   | 0:0:4.480  | John Doe | Hi, I'm John Doe, no relation, Ha! COO.                         |
		| 0:0:3.520  | 0:0:5.460  | Jane Doe | Today I want to discuss the plans for the next financial year.  |
		| 0:0:5.300  | 0:0:5.910  | Jane Doe | This year has been turbulent, next year is predicted to be too. |
		| 0:0:7.80   | 0:0:8.180  | John Doe | And the turbulence hasn't been restricted to a single region.   |
		| 0:0:8.810  | 0:0:9.500  | John Doe | It's been a global trend                                        |
		| 0:0:10.690 | 0:0:11.510 | Jane Doe | And that's what's worrisome, and why we need a plan.            |
	When I process the Transcription
	Then I should get a list of aggregated Transcription data structures with the following content:
		| start      | end        | speaker  | script                                                                                                                         |
		| 0:0:0.0    | 0:0:1.250  | Jane Doe | Hi I'm Jane Doe, CEO.                                                                                                          |
		| 0:0:2.90   | 0:0:4.480  | John Doe | Hi, I'm John Doe, no relation, Ha! COO.                                                                                        |
		| 0:0:3.520  | 0:0:5.910  | Jane Doe | Today I want to discuss the plans for the next financial year. This year has been turbulent, next year is predicted to be too. |
		| 0:0:7.80   | 0:0:9.500  | John Doe | And the turbulence hasn't been restricted to a single region. It's been a global trend                                         |
		| 0:0:10.690 | 0:0:11.510 | Jane Doe | And that's what's worrisome, and why we need a plan.                                                                           |