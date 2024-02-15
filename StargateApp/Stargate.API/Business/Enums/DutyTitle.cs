using System.ComponentModel;

namespace StargateAPI.Business.Enums;

public enum DutyTitle
{
    // TODO REVIEW: What are the correct duty titles we expect?
    [Description("Administration")]
    Administration,

    [Description("Comic Relief")]
    ComicRelief,

    [Description("Morale Booster")]
    MoraleBooster,

    [Description("Order Creator")]
    OrderCreator,

    [Description("Order Follower")]
    OrderFollower,

    [Description("RETIRED")]
    RETIRED
}