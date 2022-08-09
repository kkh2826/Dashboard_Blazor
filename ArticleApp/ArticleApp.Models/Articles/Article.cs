using Dul.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ArticleApp.Models.Articles
{
    public class Article : AuditableBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "제목을 입력하세요.")]
        public string Title { get; set; }
    }
}
