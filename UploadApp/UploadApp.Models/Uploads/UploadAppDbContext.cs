using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace UploadApp.Models.Uploads
{
    public class UploadAppDbContext : DbContext
    {
        public UploadAppDbContext()
        {

        }

        public UploadAppDbContext(DbContextOptions<UploadAppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 닷넷 프레임워크 기반에서 호출되는 코드 영역: 
            // App.config 또는 Web.config의 연결 문자열 사용
            // 직접 데이터베이스 연결문자열 설정 가능
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[
                    "ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Upload>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        public DbSet<Upload> Uploads { get; set; }
    }
}
