using MediatR;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Queries;

public record GetPeople : IRequest<GetPeopleResult>;