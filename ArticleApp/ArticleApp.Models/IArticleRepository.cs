using Dul.Domain.Common;

namespace ArticleApp.Models
{
    public interface IArticleRepository
    {
        Task<Article> AddArticleAsync(Article article); // 입력
        Task<List<Article>> GetArticlesAsync();         // 출력
        Task<Article> GetArticleByIdAsync(int id);      // 특정 개체
        Task<Article> EditArticleAsync(Article article); // 수정
        Task DeleteArticleAsync(int id);                // 삭제

        Task<PagingResult<Article>> GetAllAsync(int pageIndex, int pageSize); // 페이징 관련 메서드
    }
}
