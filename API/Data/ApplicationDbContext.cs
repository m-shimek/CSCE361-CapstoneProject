using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Models;
using Microsoft.Extensions.Configuration;

namespace MyVotingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                string? connectionString = configuration.GetConnectionString("DefaultConnection");
                if (connectionString == null)
                {
                    throw new Exception("No connection String found");
                }
                optionsBuilder.UseSqlServer(connectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Election>().HasOne(e => e.Candidate1).WithMany().HasForeignKey(e => e.Candidate1Id);

            modelBuilder.Entity<Election>().HasOne(e => e.Candidate2).WithMany().HasForeignKey(e => e.Candidate2Id);
        }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ballot> Ballots { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
