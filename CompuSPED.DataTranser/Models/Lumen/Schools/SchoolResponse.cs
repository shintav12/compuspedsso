using System.Collections.Generic;

namespace CompuSPED.DataTranser.Models.Lumen.Schools
{
    class SchoolResponse
    {
        public List<Org> orgs { get; set; }
        public List<StatusInfoSet> statusInfoSet { get; set; }
    }

    public class Org
    {
        public string sourcedId { get; set; }
        public string status { get; set; }
        public string dateLastModified { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class StatusInfoSet
    {
        public string imsx_codeMajor { get; set; }
        public string imsx_severity { get; set; }
        public string imsx_codeMinorimsx_codeMinor { get; set; }
    }
}
