namespace CompuSPED.DataTranser.Databases.CSPED
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("School")]
    public partial class School
    {
        public int SchoolId { get; set; }

        public string SchoolSourceId { get; set; }

        public int SchoolSBId { get; set; }

        public string SchoolCode { get; set; }

        public string SchoolName { get; set; }

        public string DistrcitId { get; set; }
    }
}
