using CompuSPED.Common.Base;
using CompuSPED.Common.Lumen.Responses;
using CompuSPED.Service.Base;
using CompuSPED.Service.Utils;
using System.Threading.Tasks;

namespace CompuSPED.Service.Lumen
{
    public class DemographicService : BaseService
    {
        private readonly AuthService _authService;
        public DemographicService(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<ServiceResponse<Demographics>> GetStudents(string sourceId)
        {
            string uri = $"{baseUri}/demographics/{sourceId}";
            async Task<Demographics> Func()
            {
                var auth = await _authService.Authenticate();
                var result = await uri.GetAsync<DemographicResponse>(uri, auth.Result.access_token);
                if (result.demographics == null) ThrowError("Demographic not found");
                return result.demographics;
            }
            return await ExecuteAsync(Func);
        }
    }
}
