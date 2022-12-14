using ArticleApp.Models.Articles;
using Microsoft.AspNetCore.Components;

namespace ArticleApp.Pages.Articles
{
    public partial class Create
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IArticleRepository ArticleRepository { get;set; }
        public Article Model { get; set; } = new Article();

        protected async Task btnSubmit_Click()
        {
            // 저장 로직
            await ArticleRepository.AddArticleAsync(Model);

            // 리스트 페이지 이동
            NavigationManager.NavigateTo("/Articles");
        }
    }
}
