using Dapper;
using MediatR;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Queries;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Handlers;

public class GetPersonByNameHandler : IRequestHandler<GetPersonByName, GetPersonByNameResult>
{
    private readonly IDataAccess _context;
    private readonly ILogger _logger;

    public GetPersonByNameHandler(IDataAccess context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GetPersonByNameResult> Handle(GetPersonByName request, CancellationToken cancellationToken)
    {
        var result = new GetPersonByNameResult();

        // TODO IMPORTANT: Change to LINQ
        var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id WHERE '{request.Name}' = a.Name";

        // var person = await _context.Connection.QueryAsync<PersonAstronaut>(query);
        var person = await _context.QueryAsync<PersonAstronaut>(query, cancellationToken);
        result.Person = person.FirstOrDefault();

        // Log operation success
        _logger.LogInformation("Finished creating astronaut duty");

        return result;
    }
}