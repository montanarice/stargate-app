using StargateAPI.Controllers;

namespace StargateAPI.Business.Commands;

public class CreatePersonResult : BaseResponse
{
    public int Id { get; set; }
}