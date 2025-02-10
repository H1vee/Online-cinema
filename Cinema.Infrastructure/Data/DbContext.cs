using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cinema.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Ваш рядок підключення до бази даних
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cinema;Username=mariia;Password=maha8520");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
