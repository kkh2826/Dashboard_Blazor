using Microsoft.EntityFrameworkCore;

namespace NoticeApp.Models
{
    public class NoticeAppDbContext : DbContext
    {
        public NoticeAppDbContext()
        {

        }

        public NoticeAppDbContext(DbContextOptions<NoticeAppDbContext> options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notice>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        public DbSet<Notice> Notcies { get; set; }
    }
}
