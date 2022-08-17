using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using UploadApp.Models.Uploads;
using UploadApp.Services;

namespace UploadApp.Pages.Uploads.Components
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
        public Upload Model { get; set; }

        [Parameter]
        public Action CreateCallback { get; set; }

        [Parameter]
        public EventCallback EditCallback { get; set; }

        [Inject]
        public IUploadRepository UploadRepositoryAsyncReference { get; set; }

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
            var file = selectedFiles.FirstOrDefault();
            var fileName = "";
            int fileSize = 0;
            if (file != null)
            {
                fileName = file.Name;
                fileSize = Convert.ToInt32(file.Size);
                await FileUploadServiceReference.UploadAsync(file);

                Model.FileName = fileName;
                Model.FileSize = fileSize;

            }
            if (!int.TryParse(parentId, out int newParentId))
            {
                newParentId = 0;
            }
            Model.ParentId = newParentId;

            if (Model.Id == 0)
            {
                await UploadRepositoryAsyncReference.AddAsync(Model);
                CreateCallback?.Invoke();
            }
            else
            {
                await UploadRepositoryAsyncReference.EditAsync(Model);
                await EditCallback.InvokeAsync(true);
            }

            IsShow = false;

        }

        [Inject]
        public IFileUploadService FileUploadServiceReference { get; set; }

        private IFileListEntry[] selectedFiles;
        protected void HandleSelection(IFileListEntry[] files)
        {
            this.selectedFiles = files;
        }

    }
}
