using AdministrationAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace AdministrationAPI.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Fee> Fees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fee>().HasKey(x => x.FeeId);
            modelBuilder.Entity<Fee>().Property(x => x.FeeId).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
