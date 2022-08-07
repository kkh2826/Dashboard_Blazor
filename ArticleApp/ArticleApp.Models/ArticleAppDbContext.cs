using ArticleApp.Models.Articles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleApp.Models
{
    public class ArticleAppDbContext : DbContext
    {
        // Install-Package EntityFrameworkCore.SqlServer
        // Install-Package EntityFrameworkCore.InMemory
        // Install-Package System.Configuration.ConfigurationManager
        public ArticleAppDbContext()
        {
            //Emtpy
        }

        public ArticleAppDbContext(DbContextOptions<ArticleAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Article 테이블의 Created 열은 자동으로 GetDate()로 조건 부여
            modelBuilder.Entity<Article>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        // ArticleApp 관련 모든 테이블 참조 변수
        public DbSet<Article> Articles { get; set; }
    }
}
