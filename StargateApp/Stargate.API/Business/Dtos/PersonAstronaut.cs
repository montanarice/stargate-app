using System.Text.Json.Serialization;
using StargateAPI.Business.Data;
using StargateAPI.Business.Enums;

namespace StargateAPI.Business.Dtos;

public class PersonAstronaut
{
    [JsonPropertyName("personId")]
    public int PersonId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("currentRank")]
    public Rank CurrentRank { get; set; }

    [JsonPropertyName("currentDutyTitle")]
    public DutyTitle CurrentDutyTitle { get; set; }

    [JsonPropertyName("careerStartDate")]
    public DateTime? CareerStartDate { get; set; }

    [JsonPropertyName("careerEndDate")]
    public DateTime? CareerEndDate { get; set; }

    [JsonPropertyName("astronautDuties")]
    private ICollection<AstronautDuty> AstronautDuties { get; set; } = new List<AstronautDuty>();
}