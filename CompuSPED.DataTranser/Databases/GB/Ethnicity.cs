namespace CompuSPED.DataTranser.Databases.GB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ethnicity")]
    public partial class Ethnicity
    {
        public int EthnicityID { get; set; }

        [Required]
        [StringLength(1)]
        public string EthnicityCode { get; set; }

        [Required]
        [StringLength(50)]
        public string EthnicityDesc { get; set; }
    }
}
