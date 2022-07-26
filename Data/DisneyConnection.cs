using DisneyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DisneyApi.Data
{
    public class DisneyConnection : DbContext
    {
        public DisneyConnection(DbContextOptions<DisneyConnection> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Character>().ToTable("Character");
            modelBuilder.Entity<Gender>().ToTable("Gender");
            modelBuilder.Entity<MovieCharacter>().ToTable("MovieCharacter");
            modelBuilder.Entity<MovieGender>().ToTable("MovieGender");

            modelBuilder.Entity<MovieCharacter>()
                .HasKey(c => new { c.CharacterId, c.MovieId });

            modelBuilder.Entity<MovieCharacter>()
                .HasOne(c => c.Character)
                .WithMany(mc => mc.MovieCharacters)
                .HasForeignKey(c => c.CharacterId);

            modelBuilder.Entity<MovieCharacter>()
                .HasOne(m => m.Movie)
                .WithMany(mc => mc.MovieCharacters)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<MovieGender>()
                .HasKey(g => new {   g.GenderId, g.MovieId });

            modelBuilder.Entity<MovieGender>()
                .HasOne(m => m.Movie)
                .WithMany(mg => mg.MovieGenders)
                .HasForeignKey(m => m.MovieId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Movie> Movies { get; set; }        
        public DbSet<MovieCharacter> MovieCharacters { get; set; }
        public DbSet<MovieGender> MovieGenders { get; set; }

    }
}
