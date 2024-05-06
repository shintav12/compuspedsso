using CompuSPED.Common.Lumen;
using System.Collections.Generic;

namespace CompuSPED.DataTranser.Models.Lumen.Demographics
{
    public class Demographics
    {
        public string sourcedId { get; set; }
        public string status { get; set; }
        public string dateLastModified { get; set; }
        public string birthDate { get; set; }
        public string sex { get; set; }
        public string americanIndianOrAlaskaNative { get; set; }
        public string asian { get; set; }
        public string blackOrAfricanAmerican { get; set; }
        public string nativeHawaiianOrOtherPacificIslander { get; set; }
        public string white { get; set; }
        public string demographicRaceTwoOrMoreRaces { get; set; }
        public string hispanicOrLatinoEthnicity { get; set; }
        public string stateOfBirthAbbreviation { get; set; }
        public string cityOfBirth { get; set; }
    }


    public class DemographicResponse
    {
        public Demographics demographics { get; set; }
        public List<StatusInfoSet> statusInfoSet { get; set; }
    }
}
