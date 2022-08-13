using Dul.Domain.Common;

namespace NoticeApp.Models
{

    public class NoticeRepositoryAsync : INoticeRepositoryAsync
    {
        public Task<Notice> AddAsync(Notice model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(Notice model)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notice>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagingResult<Notice>> GetAllAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResult<Notice>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
        {
            throw new NotImplementedException();
        }

        public Task<Notice> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResult<Notice>> SearchAllAsync(int pageIndex, int pageSize, string searchQuery)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResult<Notice>> SearchAllByParentIdAsync(int pageIndex, int pageSize, string searchQuery, int parentId)
        {
            throw new NotImplementedException();
        }
    }
}
