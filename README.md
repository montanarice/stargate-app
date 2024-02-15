# Stargate Webapp
This is a proof of concept .NET 7.0 application showing a range of full-stack technologies including,
- CQRS structure using MediatR.
- Abstracted data access layer (see `IDataAccess`) working with EF Core.
- Unit tests using Moq, taking advantage of the abstracted data access layer.
- RESTful API connected to SQLite database.
- A blazor front end interface making requests to the separated API.
- Logger injection and database logging.

## Application Structure
The application is separated into a layered architecture to try to avoid a monolithic structure as simply as possible.
- `Stargate.API` - This is a runnable application which supports astronaut  RESTful API interaction.
    - TODO: For another time, consider refactoring this project into `Stargate.API`, `Stargate.DataAccess`, and `Stargate.DB` projects. The API project should just be reponsible for endpoint handling and requests to MediatR. The DataAccess project should handle MediatR and abstract data access components, and `Stargate.DB` would host the actual SQLite database.
- `Stargate.Blazor` - This is a runnable application that starts up the web app, allowing for user interaction to view astronaut data.

## How to Run
In order to keep effort time under half a day, I did not set up build and deploy scripts so you will need to clone the repository and build in visual studio.
I can write those scripts later today if desired, just let me know!

- Clone the repository
- Update NuGet packages
- Build the solution
- [Optional] Initialize the database (if not included in source control) with...
    - `~/StargateApp/Stargate.API> dotnet ef migrations add InitialCreate`
    - `~/StargateApp/Stargate.API> dotnet dotnet ef database update`
- Run in visual studio as multiple startup projects, `Stargate.API` and `Stargate.Blazor`
- Perform CRUD operations on the Swagger API UI and see the updates on the Blazor webpage.

## Other Thoughts
- I left many comments in the code marked as TODO to show what I would do if not rushing everything for a proof of concept app. Feel free to check those out for discussion topics and design decisions.
- Specifically, check comments marked `// TODO CRITICAL: <some message>` for lingering things I view as top priority.
