namespace CompuSPED.DataTranser.Databases.GB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("School")]
    public partial class School
    {
        public int SchoolID { get; set; }

        public int DistrictID { get; set; }

        [Required]
        [StringLength(50)]
        public string SchoolName { get; set; }

        [Required]
        [StringLength(5)]
        public string SchoolCode { get; set; }

        [Required]
        [StringLength(200)]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(15)]
        public string Zip { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(20)]
        public string Fax { get; set; }

        [Required]
        [StringLength(50)]
        public string Principal { get; set; }

        public bool BuildingOpen { get; set; }
    }
}
