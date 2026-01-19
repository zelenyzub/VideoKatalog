using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoKlub.Models;

namespace VideoKlub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Favorite> Favorites { get; set; } 

        //USER CANT RATE ONE VIDEO MORE THEN ONCE
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Rate>()
                .HasIndex(r => new { r.VideoId, r.UserId })
                .IsUnique();

            builder.Entity<Favorite>()
                .HasIndex(f => new { f.VideoId, f.UserId })
                .IsUnique();
        }

    }
}
