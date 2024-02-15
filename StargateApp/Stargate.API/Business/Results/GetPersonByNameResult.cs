using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Results;

public class GetPersonByNameResult : BaseResponse
{
    public PersonAstronaut? Person { get; set; }
}