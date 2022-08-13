namespace NoticeApp.Models
{
    public interface INoticeRepositoryAsync : ICrudRepositoryAsync<Notice>
    {
        Task<Tuple<int, int>> GetStatus(int parentId);
        Task<bool> DeleteAllByParentId(int parentId);
    }
}
