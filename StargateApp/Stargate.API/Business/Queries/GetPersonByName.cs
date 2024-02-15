using MediatR;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Queries;

public record GetPersonByName(string Name) : IRequest<GetPersonByNameResult>;