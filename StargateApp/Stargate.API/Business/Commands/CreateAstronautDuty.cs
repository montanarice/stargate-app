using MediatR;
using System.Net;
using StargateAPI.Business.Enums;

namespace StargateAPI.Business.Commands;

public record CreateAstronautDuty(string Name, Rank Rank, DutyTitle DutyTitle, DateTime DutyStartDate)
    : IRequest<CreateAstronautDutyResult>;

// public class CreateAstronautDuty : IRequest<CreateAstronautDutyResult>
// {
//     public required string Name { get; set; }
//
//     public required string Rank { get; set; }
//
//     public required string DutyTitle { get; set; }
//
//     public DateTime DutyStartDate { get; set; }
// }