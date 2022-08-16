using Microsoft.AspNetCore.Components;
using NoticeApp.Models;

namespace UploadApp.Pages.Notices
{
    public partial class Details
    {
        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }


        [Parameter]
        public int Id { get; set; }

        protected Notice model = new Notice();

        protected string content = "";

        protected override async Task OnInitializedAsync()
        {
            model = await NoticeRepositoryAsyncReference.GetByIdAsync(Id);
            content = Dul.HtmlUtility.EncodeWithTabAndSpace(model.Content);
        }
    }
}
