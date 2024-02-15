using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;
using System.Text.Json.Serialization;

namespace StargateAPI.Business.Results;

public class GetAstronautDutiesByNameResult : BaseResponse
{
    // TODO REVIEW: Preferred way to do data access from UI perspective? Current form is making this namespace break SRP.
    [JsonPropertyName("people")]
    public PersonAstronaut? Person { get; set; }

    [JsonPropertyName("astronautDuties")]
    public List<AstronautDuty> AstronautDuties { get; set; } = new();
}