# {DULDuLAO SOLUTION example modern web api}
> For Steve to stay abreast of latest and greatest

The Duldulao solution will be the TFL Web API.  Built up over time it will contain
all the Commands and Queries needed to satisfy client apps which will include
Butler Jobs and LinqPad scripts.

Example from Linqpad which talks to the ScheduleController.

```
	var gameList = RestUtility.CallService<List<Game>>(
		$"http://katla:5100/schedule/2021/NFL/{Int32.Parse(nftDraft.Week)}") 
		as List<Game>;
```

## Architecture Descisions
  1. Target .NET 6.0
  2. use DbfDataReader library to read DBF files


## Issues
  1. How to talk to DBF files
	1. DbfDataReader can only read sequentially

## Development approach

Develop features on a Use Case basis employing TDD.

See the architecture decisions in the Doco folder for further information.

Basically the development process is one of designing Commands and testing them

### Queries so far 
 1. Get a Round (the schedule for a Week); done
 2. Get next game for a team; done

### Commands so far 
 1. 

### Building

 1. Make sure all the test pass
 2. Test new endpoints locally


### Deploying / Publishing

 1. Set to Release mode
 2. right click TflCore.WebApi
 3. Publish
 4. press Publish button
 5. Stop website in prod: AspNetCoreWebApi
 6. xcopy deploy to Prod from `c:\Users\Owner\source\repos\Duldulao\dist\` 
    to `l:\websites\AspNetCoreWebApi\`
 7. start website

## Features

 1. .

## Configuration

Here you should write what are all of the configurations a user can enter when
using the project.