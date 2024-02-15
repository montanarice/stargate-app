using Dapper;
using MediatR;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Exceptions;
using StargateAPI.Business.Queries;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Handlers;

public class GetPeopleHandler : IRequestHandler<GetPeople, GetPeopleResult>
{
    private readonly IDataAccess _context;
    private readonly ILogger _logger;
    
    public GetPeopleHandler(IDataAccess context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GetPeopleResult> Handle(GetPeople request, CancellationToken cancellationToken)
    {
        var result = new GetPeopleResult();

        // TODO: Change to LINQ
        var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id";

        // var people = await _context.Connection.QueryAsync<PersonAstronaut>(query);
        var people = await _context.QueryAsync<PersonAstronaut>(query, cancellationToken);

        result.People = people.ToList();

        // Log operation success
        _logger.LogInformation("Finished getting all people operation");

        return result;
    }
}