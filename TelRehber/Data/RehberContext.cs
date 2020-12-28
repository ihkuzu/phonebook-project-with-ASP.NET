using Microsoft.EntityFrameworkCore;

namespace TelRehber.Data
{
    public class RehberContext : DbContext
    {
        public RehberContext(DbContextOptions<RehberContext> options) : base(options)
        {
        }
        public DbSet<Models.Bilgiler> Bilgiler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Bilgiler>().ToTable("Bilgiler");
        }
    }
}
