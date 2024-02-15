using MediatR;

namespace StargateAPI.Business.Commands;

// TODO: Turn into record
public class CreatePerson : IRequest<CreatePersonResult>
{
    public required string Name { get; set; } = string.Empty;
}