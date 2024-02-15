using System.Text.Json.Serialization;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Results;

public class GetPeopleResult : BaseResponse
{
    // TODO REVIEW: Preferred way to do data access from UI perspective? Current form is making this namespace break SRP.
    [JsonPropertyName("people")]
    public List<PersonAstronaut>? People { get; set; } = new() { };

}