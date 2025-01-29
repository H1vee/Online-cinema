
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastructure.Entities;

namespace Cinema.Infrastructure.Data
{
    public class ApplicationDbContext:  DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
       public DbSet<User> Users { get; set; }
       public DbSet<UserRole> UserRoles { get; set; }
       public DbSet<UserRoleAssignment> UserRoleAssignments { get; set; }
       public DbSet<Movie> Movies { get; set; }
       public DbSet<Genre> Genres { get; set; }
       public DbSet<MovieGenre> MovieGenres { get; set; }
       public DbSet<Actor> Actors { get; set; }
       public DbSet<Role> Roles { get; set; }
       public DbSet<MovieActor> MovieActors { get; set; }
       public DbSet<Hall> Halls { get; set; }
       public DbSet<Seat> Seats { get; set; }
       public DbSet<Showtime> Showtimes { get; set; }
       public DbSet<PricingRule> PricingRules { get; set; }
       public DbSet<Sale> Sales { get; set; }
       public DbSet<Ticket> Tickets { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<UserRoleAssignment>()
               .HasKey(ura => new { ura.UserID, ura.RoleID });

           modelBuilder.Entity<MovieGenre>()
               .HasKey(mg => new { mg.MovieID, mg.GenreID });

           modelBuilder.Entity<MovieActor>()
               .HasKey(ma => new { ma.MovieID, ma.ActorID, ma.RoleID });

           modelBuilder.Entity<Ticket>()
               .HasOne(t => t.Sale)
               .WithMany(s => s.Tickets)
               .HasForeignKey(t => t.SaleID)
               .OnDelete(DeleteBehavior.SetNull);
       }
    }
}



namespace Cinema.Infrastructure.Data;

public class ApplicationDbContext
{
    
}

