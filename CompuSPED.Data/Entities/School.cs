using System.ComponentModel.DataAnnotations;

namespace CompuSPED.Data.Entities
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        public string SchoolSourceId { get; set; }
        public int SchoolSBId { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public string DistrcitId { get; set; }
    }
}
