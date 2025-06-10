[![.NET](https://github.com/Carpenteri1/JukeBox/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Carpenteri1/JukeBox/actions/workflows/dotnet.yml)
[![CodeQL Advanced](https://github.com/Carpenteri1/JukeBox/actions/workflows/codeql.yml/badge.svg)](https://github.com/Carpenteri1/JukeBox/actions/workflows/codeql.yml)

# JukeBox

### Start
-  git clone https://github.com/Carpenteri1/JukeBox.git or download JukeBox.zip
-  unzip (skip if git clone)
-  cd JukeBox
-  dotnet restore
-  dotnet build
-  dotnet run
### Install self contained

#### Windows (x64)
- dotnet publish -c Release -r win-x64 --self-contained true
#### Linux (x64)
- dotnet publish -c Release -r linux-x64 --self-contained true
#### macOS (x64)
- dotnet publish -c Release -r osx-x64 --self-contained true
#### macOS (Apple Silicon)
- dotnet publish -c Release -r osx-arm64 --self-contained true

You will find everything in JukeBox/bin/Release/net8.0/osx-arm64/publish/
A file called JukeBox will be created, which you can run.
### Ramverk
- NET Core 8
- ADO.NET
- ASP.NET
- C#
- Swagger
- Dapper
- MediaR
- SQLlite
- Poppin
- Yaml
#### .NET Core
Iv picked .NET 8, which is the latest LTS version (Long Term Support). It offers high stability, long-term support, and good performance.
.NET is my primary development language, making it a natural choice. The platform is well-established, powerful, and easy to scale up to larger systems.
#### Swagger
Swagger is a widely used tool for API documentation and testing, 
allowing developers to interact with the API directly through a web interface. 
It automatically generates documentation based on the API's endpoints, request/response models, and parameters.
Swagger also with .NET core as of .NET 8,

I know swagger will be replaced in some contexts in newer versions of .NET, but I chose to use it because:
- Its integration with .NET Core is seamless
- It provides a user-friendly interface for testing APIs
- Well known
- Personal experience and familiarity with it
#### Dapper
Iv picked dapper over ADO.NET for several reasons:
- Minimal overhead
- High performance, close to raw ADO.NET
- easy syntax and quick learning curve
- clear separation of queries in the codebase
#### MediaR
Because MediatR is a library that implements the mediator pattern and CQRS, it provides a clear separation between the API layer (controllers) and the business logic (handlers). 
This allows for:
Its easy to scale the application.
- Lose couple over components
- Scalable structure
#### SQLite
For data storage, the project uses SQLite, a lightweight, file-based database that requires minimal configuration. 
It is well-suited for a proof of concept.
#### In-Memory Caching
Project uses Microsoft.Extensions.Caching.Memory for built-in in-memory caching in .NET. It allows temporary storage of data directly in the application's memory.
#### Poppin
Poppin was chosen for its ability to re-attempt failed requests from an endpoint.
#### YAML
For building CD/CI pipelines and configuration files. YAML is a human-friendly format that is well-suited for:
Testing the code once its pushed to github, building the code so see if there is any error and also as a "code reviewer" in some cases. r.
### Error handling
Current implementation has basic error handling, but there is room for improvement:
- Some null checks are missing, which can lead to NullReferenceExceptions.
- Better endpoint error handling is needed, such as using ProblemDetails according to RFC7807.
- No test cases have been written yet, which was planned but deprioritized due to time constraints.
### Further development proposals
There are several natural development paths to build upon this proof of concept:
- Unit tests: The project currently lacks unit tests.
- Expansions on endpoints: For example, a search function based on artist names instead of just IDs. The CQRS structure makes it easy to introduce more queries with different filters.
- Flera retry-policies: Just nu används en enkel retry-policy i ArtistInfoHandler.cs. Detta kan generaliseras med Polly eller liknande bibliotek för att hantera instabil extern datahämtning.
- Better and more retry policies: Currently, a simple retry policy is used in ArtistInfoHandler.cs.
- Performance improvements: The ArtistInfo search can be optimized; currently, the response time is somewhat long before all metadata is loaded.
- UI-client: The project could benefit from a frontend (e.g., React, Vue, or Blazor) to visualize artist data and album covers in a more user-friendly way.
