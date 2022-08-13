using Dul.Domain.Common;
using Microsoft.Extensions.Logging;

namespace NoticeApp.Models
{

    public class NoticeRepositoryAsync : INoticeRepositoryAsync
    {
        private readonly NoticeAppDbContext _context;
        private readonly ILogger _logger;

        public NoticeRepositoryAsync(NoticeAppDbContext context, ILoggerFactory loggerFactory)
        {
            this._context = context;
            this._logger = loggerFactory.CreateLogger(nameof(NoticeRepositoryAsync));
        }
        public async Task<Notice> AddAsync(Notice model)
        {

            try
            {
                _context.Notices.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"에러 발생({nameof(AddAsync)}): {e.Message}");
            }

            return model;
        }

        public Task<List<Notice>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Notice> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> EditAsync(Notice model)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteAsync(int id)
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


    }
}
