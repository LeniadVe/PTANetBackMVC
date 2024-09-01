using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdministrationAPI.DTO
{
    public class Fee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeeId { get; set; }
        public string Country { get; set; }
        public float? HourlyImbalanceFee { get; set; }
        public float? ImbalanceFee { get; set; }
        public float? PeakLoadFee { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime TimestampUTC { get; set; }
        public float? VolumeFee { get; set; }
        public float? WeeklyFee { get; set; }
    }
}
