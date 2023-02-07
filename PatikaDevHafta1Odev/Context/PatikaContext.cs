using Microsoft.EntityFrameworkCore;
using PatikaDevHafta1Odev.Entities;

namespace PatikaDevHafta1Odev.Context
{
    public class PatikaContext : DbContext
    {
        public PatikaContext(DbContextOptions<PatikaContext> options)
     : base(options)
        {
            // Kendi SQL Server bağlantınızı appsettings.json üzerinden değiştirebilirsiniz.
        }

        public DbSet<Product> Products { get; set; }


    }
}
