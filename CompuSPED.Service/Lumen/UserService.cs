using CompuSPED.Common.Base;
using CompuSPED.Common.Lumen.Responses;
using CompuSPED.Common.Lumen.Users;
using CompuSPED.Common.Lumen.Enums;
using CompuSPED.Service.Base;
using CompuSPED.Service.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace CompuSPED.Service.Lumen
{
    public class UserService : BaseService
    {
        private readonly AuthService _authService;
        public UserService(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<ServiceResponse<User>> AuthUser(string email, string password)
        {
            string uri = $"{baseUri}/users?filter=email='{email}'&limit=1";
            async Task<User> Func()
            {
                var auth = await _authService.Authenticate();
                var result = await uri.GetAsync<GetAllUsersResponse>(uri, auth.Result.access_token);
                if (result.users == null) ThrowError("User or password incorrect");
                var user = result.users.First();
                if (user.password != password) ThrowError("User or password incorrect");
                if (!user.role.Equals(UserRoles.Parent) && !user.role.Equals(UserRoles.Teacher)) ThrowError("User or password incorrect");
                return user;
            }
            return await ExecuteAsync(Func);
        }

        public async Task<ServiceResponse<User>> ValidateUserEmail(string email)
        {
            string uri = $"{baseUri}/users?filter=email='{email}'&limit=1";
            async Task<User> Func()
            {
                var auth = await _authService.Authenticate();
                var result = await uri.GetAsync<GetAllUsersResponse>(uri, auth.Result.access_token);
                if (result.users == null) ThrowError("User incorrect");
                var user = result.users.First();
                if (!user.role.Equals(UserRoles.Parent) && !user.role.Equals(UserRoles.Teacher)) ThrowError("User incorrect");
                return user;
            }
            return await ExecuteAsync(Func);
        }
    }
}
