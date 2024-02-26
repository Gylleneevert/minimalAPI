global using Microsoft.EntityFrameworkCore;
using minimalAPI_webbutveckling_labb2.Models;

namespace minimalAPI_webbutveckling_labb2.Data
{
    public class DataContext : DbContext
    {


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=tcp:car-server.database.windows.net,1433;Initial Catalog=carDB;Persist Security Info=False;User ID=ithsadmin;Password=Azure12345!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Car> Cars => Set<Car>();
    }
}