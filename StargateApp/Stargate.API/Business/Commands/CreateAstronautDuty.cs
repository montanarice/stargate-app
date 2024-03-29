﻿using MediatR;
using System.Net;
using StargateAPI.Business.Enums;
using StargateAPI.Business.Results;

namespace StargateAPI.Business.Commands;

public record CreateAstronautDuty(string Name, Rank Rank, DutyTitle DutyTitle, DateTime DutyStartDate)
    : IRequest<CreateAstronautDutyResult>;