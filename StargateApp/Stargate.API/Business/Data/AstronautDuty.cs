using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using StargateAPI.Business.Enums;

namespace StargateAPI.Business.Data
{
    [Table("AstronautDuty")]
    public class AstronautDuty
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("personId")]
        public int PersonId { get; set; }

        [JsonPropertyName("rank")]
        public Rank Rank { get; set; }

        [JsonPropertyName("dutyTitle")]
        public DutyTitle DutyTitle { get; set; }

        [JsonPropertyName("dutyStartDate")]
        public DateTime DutyStartDate { get; set; }

        [JsonPropertyName("dutyEndDate")]
        public DateTime? DutyEndDate { get; set; }

        [JsonPropertyName("person")]
        public virtual Person? Person { get; set; }
    }
}
