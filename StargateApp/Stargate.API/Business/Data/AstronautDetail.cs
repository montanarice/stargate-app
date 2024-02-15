using System.ComponentModel.DataAnnotations.Schema;
using StargateAPI.Business.Enums;

namespace StargateAPI.Business.Data
{
    [Table("AstronautDetail")]
    public class AstronautDetail
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public Rank CurrentRank { get; set; }

        public DutyTitle CurrentDutyTitle { get; set; } 

        public DateTime CareerStartDate { get; set; }

        public DateTime? CareerEndDate { get; set; }

        public virtual Person? Person { get; set; }
    }
}
