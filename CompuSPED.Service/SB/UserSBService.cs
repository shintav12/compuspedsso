using CompuSPED.Common.Base;
using CompuSPED.Common.Lumen.Enums;
using CompuSPED.Common.SB.Enums;
using CompuSPED.Data;
using CompuSPED.SBData;
using CompuSPED.Service.Base;
using CompuSPED.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuSPED.Service.SB
{
    public class UserSBService : BaseService
    {
        private readonly KansasGBContext _SBcontext;
        private readonly DatabaseContext _context;
        public UserSBService(KansasGBContext SBcontext, DatabaseContext context) 
        {
            _SBcontext = SBcontext;
            _context = context;
        }

        public async Task<ServiceResponse<User>> ValidateNewUser(Common.Lumen.Users.User lumenUser)
        {
            async Task<User> Func()
            {
                var user = _SBcontext.Users.FirstOrDefault(x => x.Email.Equals(lumenUser.email));
                if (user == null)
                {
                    user = AddUser(lumenUser);
                    if(user.UserRoleID == (int)UserRolesSB.DistrictAdmin)
                    {
                        //List<string> districtsLumen = lumenUser.orgs.Where(x => x.type.Equals(OrgTypesEnum.District)).Select(x => x.sourcedId).ToList();
                        //List<int> districtsSB = _context.Districts.Where(x => districtsLumen.Contains(x.DistrictSourceId)).Select(x => x.DistrictSBId).ToList();
                        List<int> districtsSB = new List<int> { 201 };
                        AssignDistricts(user.UserID, districtsSB);
                    }
                    else
                    {
                        List<string> schoolsLumen = lumenUser.orgs.Where(x => x.type.Equals(OrgTypesEnum.School)).Select(x => x.sourcedId).ToList();
                        List<int> schoolsSB = _context.Schools.Where(x => schoolsLumen.Contains(x.SchoolSourceId)).Select(x => x.SchoolId).ToList();
                        AssignSchools(user.UserID, schoolsSB);

                    }
                }
                return user;
            }
            return await ExecuteAsync(Func);
        }

        private User AddUser(Common.Lumen.Users.User lumenUser)
        {
            var user = new User
            {
                UserRoleID = (lumenUser.role.Equals(UserRoles.Teacher)) ? (int)UserRolesSB.NormalUser : (int)UserRolesSB.DistrictAdmin,
                AccessLevel = (lumenUser.role.Equals(UserRoles.Teacher)) ? AccessLevelSB.School : AccessLevelSB.Districts,
                AccountActive = true,
                Administrator = false,
                Email = lumenUser.email,
                LastName = lumenUser.familyName,
                FirstName = lumenUser.givenName,
                Supervisor = false,
                UserStateID = 1,
                Password = HashPassword.CreateDBPassword(lumenUser.password)
            };
            _SBcontext.Users.Add(user);
            _SBcontext.SaveChanges();
            return user;
        }

        private void AssignDistricts(int userId, List<int> districts)
        {
            List<UserDistrict> dbUD = new List<UserDistrict>();
            foreach(int districtId in districts)
            {
                var userDistricts = new UserDistrict
                {
                    DistrictID = districtId,
                    UserID = userId
                };
                dbUD.Add(userDistricts);
            }
            _SBcontext.UserDistricts.AddRange(dbUD);
            _SBcontext.SaveChanges();
        }

        private void AssignSchools(int userId, List<int> schools)
        {
            List<UserSchool> dbUD = new List<UserSchool>();
            foreach (int schoolId in schools)
            {
                var userSchool = new UserSchool
                {
                    SchoolID = schoolId,
                    UserID = userId
                };
                dbUD.Add(userSchool);
            }
            _SBcontext.UserSchools.AddRange(dbUD);
            _SBcontext.SaveChanges();
        }
    }
}
