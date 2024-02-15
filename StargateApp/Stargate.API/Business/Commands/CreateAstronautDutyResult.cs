using StargateAPI.Controllers;

namespace StargateAPI.Business.Commands;

public class CreateAstronautDutyResult : BaseResponse
{
    public int? Id { get; set; }
}