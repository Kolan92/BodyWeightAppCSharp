using BodyWeightApp.DataContext.Entities;

using Microsoft.EntityFrameworkCore;

namespace BodyWeightApp.DataContext
{
    public class BodyInfoContext : DbContext
    {
        public BodyInfoContext(DbContextOptions<BodyInfoContext> options)
            : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<BodyWeight> BodyWeights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().ToTable("user_profiles");
            modelBuilder.Entity<BodyWeight>().ToTable("body_weight_measurements");
        }
    }
}
