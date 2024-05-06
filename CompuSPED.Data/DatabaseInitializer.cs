using CompuSPED.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuSPED.Data
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var students = new List<Clients>
            {
            new Clients{ClientUID = Guid.NewGuid().ToString("N") , ClientName = "CCKY", ClientSecret= "VI8auoyMIG"},
            new Clients{ClientUID = Guid.NewGuid().ToString("N") , ClientName = "CCTX", ClientSecret= "u8jCK0cgc7"}
            };

            students.ForEach(s => context.Clients.Add(s));
            context.SaveChanges();
        }
    }
}
