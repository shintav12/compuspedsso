using System.ComponentModel.DataAnnotations;

namespace CompuSPED.Data.Entities
{
    public class Districts
    {
        [Key]
        public int DistrictId { get; set; }
        public int DistrictSBId { get; set; }
        public string DistrictSourceId { get; set; }
        public string DistrcitName { get; set; }
        public string DistrictCode { get; set; }
    }
}
