using CompuSPED.Common.Base;
using CompuSPED.Common.Lumen.Responses;
using CompuSPED.Common.Lumen.Users;
using CompuSPED.Service.Base;
using CompuSPED.Service.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompuSPED.Service.Lumen
{
    public class StudentService : BaseService
    {
        private readonly AuthService _authService;
        public StudentService(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<ServiceResponse<List<User>>> GetStudents()
        {
            string uri = $"{baseUri}/users";
            async Task<List<User>> Func()
            {
                var auth = await _authService.Authenticate();
                var result = await uri.GetAsync<GetAllUsersResponse>(uri, auth.Result.access_token);
                if (result.users == null) ThrowError("User or password incorrect");
                return result.users;
            }
            return await ExecuteAsync(Func);
        }
    }
}
