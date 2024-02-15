using MediatR;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Exceptions;

namespace StargateAPI.Business.Handlers;

// TODO FEATURE: Allow option of create person with parameterized start date
public class CreatePersonHandler : IRequestHandler<CreatePerson, CreatePersonResult>
{
    private readonly IDataAccess _context;
    private readonly ILogger _logger;

    public CreatePersonHandler(IDataAccess context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CreatePersonResult> Handle(CreatePerson request, CancellationToken cancellationToken)
    {
        // TODO: Code smell in this test? Having trouble testing this using Mocks due to database state update. Can fix with correct usage of IDataAccess
        // TODO define and catch specific business rules and chose to throw or ignore.

        // Initialize the DTOs
        var newPerson = new Person { Name = request.Name };
        var newDetail = new AstronautDetail
        {
            CareerStartDate = DateTime.Now,
            Person = newPerson,
            PersonId = newPerson.Id
        };

        // Precondition: Person must not already exist
        if (_context.People.Select(p => p.Name).Contains(request.Name))
        {
            var ex = new BusinessRuleBrokenException("Rule 1: Attempted to create a person with already existing name.");
            _logger.LogError(exception: ex, message: "Duplicate name");

            // TODO CRITICAL: Return correct endpoint error code (not 200)
            return new CreatePersonResult()
            {
                Id = newPerson.Id
            };
        }

        // Add to Person data store
        await _context.AddAsync(newPerson, cancellationToken);
        // await _context.People.AddAsync(newPerson, cancellationToken);

        // Add to Astronaut Detail data store
        // TODO: Currently assigns a default duty and rank due to use of enum. Consider a "not assigned" enum.
        await _context.AddAsync(newDetail, cancellationToken);

        // Update
        await _context.UpdateAsync(cancellationToken);

        // Log operation success
        _logger.LogInformation("Finished creating person");

        return new CreatePersonResult()
        {
            Id = newPerson.Id
        };
    }
}