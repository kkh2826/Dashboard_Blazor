using Microsoft.AspNetCore.Components;
using UploadApp.Models.Uploads;

namespace UploadApp.Pages.Uploads
{
    public partial class Details
    {
        [Inject]
        public IUploadRepository UploadRepositoryAsyncReference { get; set; }


        [Parameter]
        public int Id { get; set; }

        protected Upload model = new Upload();

        protected string content = "";

        protected override async Task OnInitializedAsync()
        {
            model = await UploadRepositoryAsyncReference.GetByIdAsync(Id);
            content = Dul.HtmlUtility.EncodeWithTabAndSpace(model.Content);
        }
    }
}
