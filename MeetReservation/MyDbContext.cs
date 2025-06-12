using MeetReservation.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetReservation
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
    {
        public DbSet<MeetEntity> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
