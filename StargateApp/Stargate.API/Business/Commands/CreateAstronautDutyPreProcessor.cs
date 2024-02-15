using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.Commands;

public class CreateAstronautDutyPreProcessor : IRequestPreProcessor<CreateAstronautDuty>
{
    private readonly IDataAccess _context;
    private readonly ILogger _logger;

    public CreateAstronautDutyPreProcessor(IDataAccess context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task Process(CreateAstronautDuty request, CancellationToken cancellationToken)
    {
        var person = _context.People.AsNoTracking().FirstOrDefault(z => z.Name == request.Name);

        if (person is null) throw new BadHttpRequestException("Bad Request");

        var verifyNoPreviousDuty = _context.AstronautDuties.FirstOrDefault(z => 
            z.DutyTitle == request.DutyTitle && 
            z.DutyStartDate == request.DutyStartDate);

        // TODO: This might be buggy
        if (verifyNoPreviousDuty is not null) throw new BadHttpRequestException("Bad Request");

        return Task.CompletedTask;
    }
}