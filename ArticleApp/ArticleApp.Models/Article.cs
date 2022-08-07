using System.ComponentModel.DataAnnotations;

namespace ArticleApp.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
