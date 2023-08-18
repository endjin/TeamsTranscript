Feature: TranscriptionExtensions

I want to be able to easily process or transform collections of Transcription data structures
So that I can better perform information extraction processes.

@tag1
Scenario: Generate a distinct list of participants from a Transcription
	Given I have the following transcription entries:
		| start      | end        | speaker       | script                                                          |
		| 0:0:0.0    | 0:0:1.250  | Joe MacMillan | Hi I'm Jane Doe, CEO.                                           |
		| 0:0:2.90   | 0:0:4.480  | Gordon Clark  | Hi, I'm John Doe, no relation, Ha! COO.                         |
		| 0:0:3.520  | 0:0:5.460  | Cameron Howe  | Today I want to discuss the plans for the next financial year.  |
		| 0:0:5.300  | 0:0:5.910  | Donna Clark   | This year has been turbulent, next year is predicted to be too. |
		| 0:0:7.80   | 0:0:8.180  | John Bosworth | And the turbulence hasn't been restricted to a single region.   |
		| 0:0:8.810  | 0:0:9.500  | Cameron Howe  | It's been a global trend                                        |
		| 0:0:10.690 | 0:0:11.510 | Joe MacMillan | And that's what's worrisome, and why we need a plan.            |
	When I ask for a distinct list of participants
	Then I should get a list of participants with the following content:
		| name          |
		| Cameron Howe  |
		| Donna Clark   |
		| Gordon Clark  |
		| Joe MacMillan |
		| John Bosworth |

Scenario: Generate a comma separated list of participants from a Transcription
	Given I have the following transcription entries:
		| start      | end        | speaker       | script                                                          |
		| 0:0:0.0    | 0:0:1.250  | Joe MacMillan | Hi I'm Jane Doe, CEO.                                           |
		| 0:0:2.90   | 0:0:4.480  | Gordon Clark  | Hi, I'm John Doe, no relation, Ha! COO.                         |
		| 0:0:3.520  | 0:0:5.460  | Cameron Howe  | Today I want to discuss the plans for the next financial year.  |
		| 0:0:5.300  | 0:0:5.910  | Donna Clark   | This year has been turbulent, next year is predicted to be too. |
		| 0:0:7.80   | 0:0:8.180  | John Bosworth | And the turbulence hasn't been restricted to a single region.   |
		| 0:0:8.810  | 0:0:9.500  | Cameron Howe  | It's been a global trend                                        |
		| 0:0:10.690 | 0:0:11.510 | Joe MacMillan | And that's what's worrisome, and why we need a plan.            |
	When I ask for a comma separated list of participants
	Then I should get a comma separated list of participants with the following content:
		| list                                                                  |
		| Cameron Howe, Donna Clark, Gordon Clark, Joe MacMillan, John Bosworth |

Scenario: Filter Transcription by timespan range
	Given I have the following transcription entries:
		| start      | end        | speaker       | script                                                          |
		| 0:1:0.0    | 0:1:1.250  | Joe MacMillan | Hi I'm Jane Doe, CEO.                                           |
		| 0:2:2.90   | 0:2:4.480  | Gordon Clark  | Hi, I'm John Doe, no relation, Ha! COO.                         |
		| 0:3:3.520  | 0:3:5.460  | Cameron Howe  | Today I want to discuss the plans for the next financial year.  |
		| 0:4:5.300  | 0:4:5.910  | Donna Clark   | This year has been turbulent, next year is predicted to be too. |
		| 0:5:7.80   | 0:5:8.180  | John Bosworth | And the turbulence hasn't been restricted to a single region.   |
		| 0:6:8.810  | 0:6:9.500  | Cameron Howe  | It's been a global trend                                        |
		| 0:7:10.690 | 0:7:11.510 | Joe MacMillan | And that's what's worrisome, and why we need a plan.            |
When I ask for transcriptions between 0:3:0.0 and 0:6:0.0
	Then I should get the following transcriptions:
		| start     | end       | speaker       | script                                                          |
		| 0:3:3.520 | 0:3:5.460 | Cameron Howe  | Today I want to discuss the plans for the next financial year.  |
		| 0:4:5.300 | 0:4:5.910 | Donna Clark   | This year has been turbulent, next year is predicted to be too. |
		| 0:5:7.80  | 0:5:8.180 | John Bosworth | And the turbulence hasn't been restricted to a single region.   |

Scenario: Group Transcriptions by 1 minute timespan
	Given I have the following transcription entries:
		| start      | end        | speaker       | script                                                          |
		| 0:1:0.0    | 0:1:1.250  | Joe MacMillan | Hi I'm Jane Doe, CEO.                                           |
		| 0:2:2.90   | 0:2:4.480  | Gordon Clark  | Hi, I'm John Doe, no relation, Ha! COO.                         |
		| 0:3:3.520  | 0:3:5.460  | Cameron Howe  | Today I want to discuss the plans for the next financial year.  |
		| 0:4:5.300  | 0:4:5.910  | Donna Clark   | This year has been turbulent, next year is predicted to be too. |
		| 0:5:7.80   | 0:5:8.180  | John Bosworth | And the turbulence hasn't been restricted to a single region.   |
		| 0:6:8.810  | 0:6:9.500  | Cameron Howe  | It's been a global trend                                        |
		| 0:7:10.690 | 0:7:11.510 | Joe MacMillan | And that's what's worrisome, and why we need a plan.            |
	When I ask for transcriptions grouped by 0:1:0.0
	Then I should get the following grouped transcriptions:
	| list | start      | end        | speaker       | script                                                          |
	| 0    | 0:1:0.0    | 0:1:1.250  | Joe MacMillan | Hi I'm Jane Doe, CEO.                                           |
	| 1    | 0:2:2.90   | 0:2:4.480  | Gordon Clark  | Hi, I'm John Doe, no relation, Ha! COO.                         |
	| 2    | 0:3:3.520  | 0:3:5.460  | Cameron Howe  | Today I want to discuss the plans for the next financial year.  |
	| 3    | 0:4:5.300  | 0:4:5.910  | Donna Clark   | This year has been turbulent, next year is predicted to be too. |
	| 4    | 0:5:7.80   | 0:5:8.180  | John Bosworth | And the turbulence hasn't been restricted to a single region.   |
	| 5    | 0:6:8.810  | 0:6:9.500  | Cameron Howe  | It's been a global trend                                        |
	| 6    | 0:7:10.690 | 0:7:11.510 | Joe MacMillan | And that's what's worrisome, and why we need a plan.            |

Scenario: Group Transcriptions by 2 minute timespan
	Given I have the following transcription entries:
		| start      | end        | speaker       | script                                                          |
		| 0:1:0.0    | 0:1:1.250  | Joe MacMillan | Hi I'm Jane Doe, CEO.                                           |
		| 0:2:0.0    | 0:2:0.000  | Gordon Clark  | Hi, I'm John Doe, no relation, Ha! COO.                         |
		| 0:3:3.520  | 0:3:5.460  | Cameron Howe  | Today I want to discuss the plans for the next financial year.  |
		| 0:4:5.300  | 0:4:12.910 | Donna Clark   | This year has been turbulent, next year is predicted to be too. |
		| 0:5:7.80   | 0:5:9.180  | John Bosworth | And the turbulence hasn't been restricted to a single region.   |
		| 0:6:8.810  | 0:6:9.500  | Cameron Howe  | It's been a global trend                                        |
		| 0:7:10.690 | 0:7:12.510 | Joe MacMillan | And that's what's worrisome, and why we need a plan.            |
	When I ask for transcriptions grouped by 0:2:0.0
	Then I should get the following grouped transcriptions:
	| list | start      | end        | speaker       | script                                                          |
	| 0    | 0:1:0.0    | 0:1:1.250  | Joe MacMillan | Hi I'm Jane Doe, CEO.                                           |
	| 1    | 0:2:0.0    | 0:2:0.000  | Gordon Clark  | Hi, I'm John Doe, no relation, Ha! COO.                         |
	| 1    | 0:3:3.520  | 0:3:5.460  | Cameron Howe  | Today I want to discuss the plans for the next financial year.  |
	| 2    | 0:4:5.300  | 0:4:12.910 | Donna Clark   | This year has been turbulent, next year is predicted to be too. |
	| 2    | 0:5:7.80   | 0:5:9.180  | John Bosworth | And the turbulence hasn't been restricted to a single region.   |
	| 3    | 0:6:8.810  | 0:6:9.500  | Cameron Howe  | It's been a global trend                                        |
	| 3    | 0:7:10.690 | 0:7:12.510 | Joe MacMillan | And that's what's worrisome, and why we need a plan.            |