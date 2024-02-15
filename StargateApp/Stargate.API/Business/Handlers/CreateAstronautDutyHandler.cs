using Dapper;
using MediatR;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Enums;
using StargateAPI.Business.Exceptions;

namespace StargateAPI.Business.Handlers;

public class CreateAstronautDutyHandler : IRequestHandler<CreateAstronautDuty, CreateAstronautDutyResult>
{
    private readonly StargateContext _context;
    private readonly ILogger _logger;

    // TODO: Refactor to use IDataAccess
    public CreateAstronautDutyHandler(StargateContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CreateAstronautDutyResult> Handle(CreateAstronautDuty request, CancellationToken cancellationToken)
    {
        // TODO: This method feels smelly and too big. Investigate and test thoroughly
        // TODO CRITICAL: Many business rules to guard here. Probably missed a few, come back to this ASAP

        try
        {
            // Get the person and their relevant data 
            var person = _context.People.FirstOrDefault(person => person.Name.Equals(request.Name));
            if (person is null)
            {
                throw new BusinessRuleBrokenException(
                    "No name was found matching the name in the attempted assigned duty.");
            }

            var personId = person.Id;
            var duties = _context.AstronautDuties
                .Where(ad => ad.PersonId == personId)
                .ToList();

            // TODO: More robust query
            var details = _context.AstronautDetails
                .First(ad => ad.PersonId == personId);

            // Rule 03 Check: No date overlap
            var dutyStartDates = duties.Select(d => d.DutyStartDate);

            if (dutyStartDates.Any(previousAssignments => request.DutyStartDate < previousAssignments))
            {
                var ex = new BusinessRuleBrokenException(
                    "Rule 03: Tried to assign a duty starting before a currently existing duty assignment");
                _logger.LogError(ex, message: $"Rule 03 broken in {GetType()}");
                throw ex;
            }

            // Create the new duty DTO 
            var newDuty = new AstronautDuty
            {
                DutyStartDate = request.DutyStartDate,
                DutyTitle = request.DutyTitle,
                Rank = request.Rank,
                // TODO: Make sure EF Core can derive these correctly with foreign key
                Person = person,
                PersonId = personId
            };

            // Handle if previously assigned duties 
            if (duties.Any())
            {
                // Update previous duty end date
                var mostRecent = duties
                    .OrderByDescending(ad => ad.DutyStartDate)
                    .First();

                // Verify rule 04
                if (mostRecent.DutyEndDate is not null)
                {
                    var ex = new BusinessRuleBrokenException(
                        "Rule 04: Most recent duty had an end date when trying to add a new duty.");
                    _logger.LogError(ex, message: $"Rule 04 broken in {GetType()}");
                    throw ex;
                }

                // Set previous duty end date
                mostRecent.DutyEndDate = request.DutyStartDate.AddDays(-1).Date;
                _context.AstronautDuties.Update(mostRecent);
            }
            
            // Handle retired case 
            if (request.DutyTitle == DutyTitle.RETIRED)
            {
                details.CareerEndDate = request.DutyStartDate.AddDays(-1).Date;
            }

            /* Handle general cases */
            // Update astronaut details 
            details.CurrentDutyTitle = request.DutyTitle;
            details.CurrentRank = request.Rank;

            // Add the new duty
            await _context.AstronautDuties.AddAsync(newDuty, cancellationToken);

            // Finally, save and update the database
            await _context.SaveChangesAsync(cancellationToken);

            // Log operation success
            _logger.LogInformation("Finished creating astronaut duty");

            // Hand back to MediatR 
            return new CreateAstronautDutyResult()
            {
                Id = newDuty.Id
            };
        }
        catch (BusinessRuleBrokenException)
        {
            throw new NotImplementedException();
        }
    }
}