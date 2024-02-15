using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.Commands;

public class CreatePersonPreProcessor : IRequestPreProcessor<CreatePerson>
{
    private readonly IDataAccess _context;

    public CreatePersonPreProcessor(IDataAccess context)
    {
        _context = context;
    }

    public Task Process(CreatePerson request, CancellationToken cancellationToken)
    {
        var person = _context.People.AsNoTracking().FirstOrDefault(z => z.Name == request.Name);

        if (person is not null) throw new BadHttpRequestException("Bad Request");

        return Task.CompletedTask;
    }
}