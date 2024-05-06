namespace CompuSPED.DataTranser.Databases.GB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("District")]
    public partial class District
    {
        public int DistrictID { get; set; }

        [Required]
        [StringLength(50)]
        public string DistrictName { get; set; }

        [StringLength(100)]
        public string DistrictNameLong { get; set; }

        [Required]
        [StringLength(5)]
        public string DistrictCode { get; set; }

        [StringLength(200)]
        public string StreetAddress { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(15)]
        public string Zip { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(5)]
        public string CoopCode { get; set; }

        [StringLength(1)]
        public string EligibilityFlag { get; set; }

        public bool? IEPFlag { get; set; }

        public bool? AdHocReporting { get; set; }

        public bool? Dashboards { get; set; }
    }
}
