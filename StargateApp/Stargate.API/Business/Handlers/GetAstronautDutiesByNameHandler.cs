using Dapper;
using MediatR;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Queries;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Handlers;

public class GetAstronautDutiesByNameHandler : IRequestHandler<GetAstronautDutiesByName, GetAstronautDutiesByNameResult>
{
    private readonly StargateContext _context;
    private readonly ILogger _logger;

    // TODO: Refactor to use IDataAccess
    public GetAstronautDutiesByNameHandler(StargateContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GetAstronautDutiesByNameResult> Handle(GetAstronautDutiesByName request, CancellationToken cancellationToken)
    {
        var result = new GetAstronautDutiesByNameResult();
        
        // TODO IMPORTANT: Change to LINQ
        var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id WHERE \'{request.Name}\' = a.Name";
        
        var person = await _context.Connection.QueryFirstOrDefaultAsync<PersonAstronaut>(query);
        result.Person = person!;

        // TODO IMPORTANT: Change to LINQ
        query = $"SELECT * FROM [AstronautDuty] WHERE {person!.PersonId} = PersonId Order By DutyStartDate Desc";

        var duties = (await _context.Connection.QueryAsync<AstronautDuty>(query)).ToList();
        result.AstronautDuties = duties;

        // TODO IMPORTANT: Return correct response code if person has no assignments
        if (!duties.Any())
        {
            _logger.LogInformation("Requested duties from person with no assigned duties.");
        }

        // Log operation success
        _logger.LogInformation("Finished getting astronaut duty by name");

        return result;

    }
}