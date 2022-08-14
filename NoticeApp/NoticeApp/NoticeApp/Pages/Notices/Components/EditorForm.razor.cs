using Microsoft.AspNetCore.Components;
using NoticeApp.Models;

namespace NoticeApp.Pages.Notices.Components
{
    public partial class EditorForm
    {
        private bool IsShow = false;
        private string parentId = "0";

        public void Show()
        {
            IsShow = true;
        }

        public void Hide()
        {
            IsShow = false;
        }

        [Parameter]
        public RenderFragment EditorFormTitle { get; set; }

        [Parameter]
        public Notice Model { get; set; }

        [Parameter]
        public Action CreateCallback { get; set; }

        [Parameter]
        public EventCallback EditCallback { get; set; }

        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

        protected int[] parentIds = { 1, 2, 3 };


        protected override void OnParametersSet()
        {
            parentId = Model.ParentId.ToString();
            if (parentId == "0")
            {
                parentId = "";
            }
        }

        protected async void CreateOrEditClick()
        {
            if (int.TryParse(parentId, out int newParentId))
            {
                newParentId = 0;
            }
            Model.ParentId = newParentId;

            if (Model.Id == 0)
            {
                await NoticeRepositoryAsyncReference.AddAsync(Model);
                CreateCallback?.Invoke();
            }
            else
            {
                await NoticeRepositoryAsyncReference.EditAsync(Model);
                await EditCallback.InvokeAsync(true);
            }

            IsShow = false;

        }
  
    }
}
