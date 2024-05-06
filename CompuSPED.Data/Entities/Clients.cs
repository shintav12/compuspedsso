using System.ComponentModel.DataAnnotations;

namespace CompuSPED.Data.Entities
{
    public class Clients
    {
        [Key]
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientUID { get; set; }
        public string ClientSecret { get; set; }
        public string ClientCode { get; set; }
        public string ClientASC { get; set; }
        public string ClientIssuer { get; set; }
    }
    
}
