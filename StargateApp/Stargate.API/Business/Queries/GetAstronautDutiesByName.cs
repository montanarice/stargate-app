using MediatR;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Queries;

public record GetAstronautDutiesByName(string Name) : IRequest<GetAstronautDutiesByNameResult>;