using CompuSPED.Common.Lumen.Users;
using System.Collections.Generic;

namespace CompuSPED.Common.Lumen.Responses
{
    public  class GetAllUsersResponse
    {
        public List<User> users { get; set; }
        public List<StatusInfoSet> StatusInfoSet { get; set; }
    }
}
