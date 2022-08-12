using ArticleApp.Models.Articles;
using Dul.Domain.Common;
using Dul.Web;
using Microsoft.AspNetCore.Components;

namespace ArticleApp.Pages.Articles
{
    public partial class Index
    {
        [Inject]
        public IArticleRepository ArticleRepository { get; set; }

        // 페이저 기본값 설정
        private PagerBase pager = new PagerBase()
        {
            PageNumber = 1,
            PageIndex = 0,
            PageSize = 2,
            PagerButtonCount = 5
        };

        private List<Article> articles;

        protected override async Task OnInitializedAsync()
        {
            // 전체 데이터 모두 출력
            //articles = await ArticleRepository.GetArticlesAsync();

            // 페이징 처리된 데이터만 출력
            PagingResult<Article> pagingData = await ArticleRepository.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = pagingData.TotalRecords; // 총 레코드 수
            articles = pagingData.Records.ToList(); // 페이징 처리된 레코드
        }

        private async void PageIndexChanged(int pageIndex)
        {
            pager.PageIndex = pageIndex;
            pager.PageNumber = pageIndex + 1;

            var pagingData = await ArticleRepository.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = pagingData.TotalRecords; // 총 레코드 수
            articles = pagingData.Records.ToList(); // 페이징 처리된 레코드

            StateHasChanged();
        }
    }
}
