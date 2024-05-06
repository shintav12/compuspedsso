namespace CompuSPED.SBData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int UserID { get; set; }

        public int? UserRoleID { get; set; }

        public int? ServiceID { get; set; }

        public bool AccountActive { get; set; }

        public bool Administrator { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public byte[] Password { get; set; }

        public bool Supervisor { get; set; }

        [Required]
        [StringLength(10)]
        public string AccessLevel { get; set; }

        public int ConsecutiveFailedLogins { get; set; }

        public bool ForcePasswordChange { get; set; }

        public DateTime? LastPasswordChange { get; set; }

        public int? UserStateID { get; set; }

        [StringLength(5)]
        public string CoopCode { get; set; }

        [Column(TypeName = "text")]
        public string Comments { get; set; }

        public bool? UserAgreement { get; set; }

        public DateTime? AgreementDateEntered { get; set; }

        public string QlikUserName { get; set; }
    }
}
