namespace CompuSPED.SBData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRole")]
    public partial class UserRole
    {
        public int UserRoleID { get; set; }

        [Required]
        [StringLength(25)]
        public string UserRoleDesc { get; set; }

        public int UserRank { get; set; }
    }
}
