using Cinema.Infrastructure.Entities;

namespace Cinema.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Actor> Actors { get; set; } = null!;
        public DbSet<Genres> Genres { get; set; } = null!;
        public DbSet<Halls> Halls { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<MovieActors> MovieActors { get; set; } = null!;
        public DbSet<MovieGenres> MovieGenres { get; set; } = null!;
        public DbSet<Roles> Roles { get; set; } = null!;
        public DbSet<Seat> Seat { get; set; } = null!;
        public DbSet<ShowTimes> ShowTimes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserRoles> UserRoles { get; set; } = null!;
        public DbSet<UserRolesAssignments> UserRolesAssignments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Actor - Movie through MovieActors
            modelBuilder.Entity<MovieActors>()
                .HasKey(ma => new { ma.ActorId, ma.MovieId, ma.RoleId });

            modelBuilder.Entity<MovieActors>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            modelBuilder.Entity<MovieActors>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActors>()
                .HasOne(ma => ma.Roles)
                .WithMany(r => r.MovieActors)
                .HasForeignKey(ma => ma.RoleId);

            // Movie - Genres through MovieGenres
            modelBuilder.Entity<MovieGenres>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenres>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenres>()
                .HasOne(mg => mg.Genres)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            // Halls - Seat (One-to-Many)
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Halls)
                .WithMany(h => h.Seat)
                .HasForeignKey(s => s.HallId);

            // ShowTimes
            modelBuilder.Entity<ShowTimes>()
                .HasKey(st => new { st.HallId, st.MovieId });

            modelBuilder.Entity<ShowTimes>()
                .HasOne(st => st.Halls)
                .WithMany(h => h.ShowTimes)
                .HasForeignKey(st => st.HallId);

            modelBuilder.Entity<ShowTimes>()
                .HasOne(st => st.Movie)
                .WithMany(m => m.ShowTimes)
                .HasForeignKey(st => st.MovieId);

            // User - UserRoles through UserRolesAssignments
            modelBuilder.Entity<UserRolesAssignments>()
                .HasKey(ura => new { ura.UserId, ura.RoleId });

            modelBuilder.Entity<UserRolesAssignments>()
                .HasOne(ura => ura.User)
                .WithMany(u => u.UserRolesAssignments)
                .HasForeignKey(ura => ura.UserId);

            modelBuilder.Entity<UserRolesAssignments>()
                .HasOne(ura => ura.UserRoles)
                .WithMany(ur => ur.UserRolesAssignments)
                .HasForeignKey(ura => ura.RoleId);
        }
    }
}
