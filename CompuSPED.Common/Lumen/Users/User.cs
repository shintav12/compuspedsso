using System.Collections.Generic;

namespace CompuSPED.Common.Lumen.Users
{
    public class User
    {
        public string sourcedId { get; set; }
        public string status { get; set; }
        public string dateLastModified { get; set; }
        public string username { get; set; }
        public string enabledUser { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string middleName { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string sms { get; set; }
        public string phone { get; set; }
        public List<Agent> agents { get; set; }
        public List<Org> orgs { get; set; }
        public string password { get; set; }
        public List<UserId> userIds { get; set; }
        public string identifier { get; set; }
        public string grades { get; set; }
    }

}
