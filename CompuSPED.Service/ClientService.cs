using CompuSPED.Common.Base;
using CompuSPED.Data;
using CompuSPED.Data.Entities;
using CompuSPED.Service.Base;
using System.Linq;
using System.Threading.Tasks;

namespace CompuSPED.Service
{
    public class ClientService : BaseService
    {
        private readonly DatabaseContext _context;
        public ClientService(DatabaseContext context) 
        {
            _context = context;
        }

        public async Task<ServiceResponse<Clients>> ValidateClientId(string clientId)
        {
            async Task<Clients> Func()
            {
                var databaseClient = _context.Clients.FirstOrDefault(x => x.ClientUID.Equals(clientId));
                if (databaseClient == null) ThrowError("Unauthorized Client Credentials");
                return databaseClient;
            };
            return await ExecuteAsync(Func);
        }

        public async Task<ServiceResponse<Clients>> ValidateClientIdAndSecret(string clientId, string secret)
        {
            async Task<Clients> Func()
            {
                var databaseClient = _context.Clients.FirstOrDefault(x => x.ClientUID.Equals(clientId) && x.ClientSecret.Equals(secret));
                if (databaseClient == null) ThrowError("Unauthorized Client Credentials");
                return databaseClient;
            };
            return await ExecuteAsync(Func);
        }

        public async Task<ServiceResponse<Clients>> ValidateIssuerAbdACS(string issuer, string ASC)
        {
            async Task<Clients> Func()
            {
                var databaseClient = _context.Clients.FirstOrDefault(x => x.ClientIssuer.Equals(issuer) && x.ClientASC.Equals(ASC));
                if (databaseClient == null) ThrowError("Unauthorized Client Credentials");
                return databaseClient;
            };
            return await ExecuteAsync(Func);
        }
    }
}
