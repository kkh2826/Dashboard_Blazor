namespace UploadApp.Models.Uploads
{
    public interface IUploadRepository : ICrudRepository<Upload>
    {
        Task<Tuple<int, int>> GetStatus(int parentId);
        Task<bool> DeleteAllByParentId(int parentId);
        Task<SortedList<int, double>> GetMonthlyCreateCountAsync();
    }
}
