using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructureta2.EF
{
    public class NTQDbContextFactory : IDesignTimeDbContextFactory<NTQDbContext>
    {
        public NTQDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("NTQDb");


            var optionsBuilder = new DbContextOptionsBuilder<NTQDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new NTQDbContext(optionsBuilder.Options);
        }
    }
}
