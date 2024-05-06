namespace CompuSPED.SBData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public int StudentID { get; set; }

        public int DistrictID { get; set; }

        public int? SchoolID { get; set; }

        public int? ResidentDistrictID { get; set; }

        public int? EthnicityID { get; set; }

        public int? GradeID { get; set; }

        public int? TransportationTypeID { get; set; }

        public int? TransportationProviderID { get; set; }

        public int? FundingTypeID { get; set; }

        public int? PrimaryEducationalSettingID { get; set; }

        public bool ActiveFlag { get; set; }

        [StringLength(50)]
        public string StudentNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string UIC { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        public string MiddleInitial { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(2)]
        public string SState { get; set; }

        [Required]
        [StringLength(50)]
        public string Zip { get; set; }

        public bool TransportationFlag { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReferralDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InitialIEPDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastIEPDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastDetIEPDate { get; set; }

        public double? SpecEdHours { get; set; }

        public double? TotalHours { get; set; }

        [StringLength(100)]
        public string ParentName { get; set; }

        public bool MedicaidEligible { get; set; }

        [StringLength(20)]
        public string MedicaidBeneficiaryID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MedicaidParentRefusalDate { get; set; }

        public bool MedicaidBillable { get; set; }

        [StringLength(25)]
        public string MedicaidLastName { get; set; }

        [StringLength(25)]
        public string MedicaidFirstName { get; set; }

        [StringLength(1)]
        public string MedicaidMI { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MedicaidDOB { get; set; }

        [StringLength(11)]
        public string SSN { get; set; }

        public bool? MedicaidParentConsent { get; set; }

        [StringLength(1)]
        public string MedicaidGender { get; set; }

        [StringLength(15)]
        public string Bus1 { get; set; }

        [StringLength(15)]
        public string Bus2 { get; set; }

        public int? SpecializedTransportationServiceTypeID { get; set; }

        public bool? CaseManagement { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CaseManagementDate { get; set; }

        public bool? ParentNotification { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ParentNotificationDate { get; set; }

        public string Comments { get; set; }

        public bool? Student504 { get; set; }

        public bool? IEPFlag { get; set; }

        public bool? OHC { get; set; }

        public bool? OHCPrimary { get; set; }
    }
}
