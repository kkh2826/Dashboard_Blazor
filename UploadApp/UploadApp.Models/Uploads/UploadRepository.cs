﻿using Dul.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UploadApp.Models.Uploads
{

    public class UploadRepository : IUploadRepository
    {
        private readonly UploadAppDbContext _context;
        private readonly ILogger _logger;

        public UploadRepository(UploadAppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger(nameof(UploadRepository));
        }
        public async Task<Upload> AddAsync(Upload model)
        {

            try
            {
                _context.Uploads.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"에러 발생({nameof(AddAsync)}): {e.Message}");
            }

            return model;
        }

        public async Task<List<Upload>> GetAllAsync()
        {
            return await _context.Uploads.OrderByDescending(m => m.Id)
                //.Include(m => m.UploadsComments)
                .ToListAsync();
        }
        public async Task<Upload> GetByIdAsync(int id)
        {
            return await _context.Uploads
                //.Include(m => m.UploadsComments)
                .SingleOrDefaultAsync(m => m.Id == id);
        }
        public async Task<bool> EditAsync(Upload model)
        {
            try
            {
                _context.Uploads.Attach(model);
                _context.Entry(model).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR({nameof(EditAsync)}): {e.Message}");
            }

            return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            //var model = await _context.Uploads.SingleOrDefaultAsync(m => m.Id == id);
            try
            {
                var model = await _context.Uploads.FindAsync(id);
                _context.Remove(model);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR({nameof(DeleteAsync)}): {e.Message}");
            }

            return false;
        }



        public async Task<PagingResult<Upload>> GetAllAsync(int pageIndex, int pageSize)
        {
            var totalRecords = await _context.Uploads.CountAsync();
            var models = await _context.Uploads
                .OrderByDescending(m => m.Id)
                //.Include(m => m.UploadsComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Upload>(models, totalRecords);
        }

        public async Task<PagingResult<Upload>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
        {
            var totalRecords = await _context.Uploads.Where(m => m.ParentId == parentId).CountAsync();
            var models = await _context.Uploads
                .Where(m => m.ParentId == parentId)
                .OrderByDescending(m => m.Id)
                //.Include(m => m.UploadsComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Upload>(models, totalRecords);
        }

        public async Task<Tuple<int, int>> GetStatus(int parentId)
        {
            var totalRecords = await _context.Uploads.Where(m => m.ParentId == parentId).CountAsync();
            var pinnedRecords = await _context.Uploads.Where(m => m.ParentId == parentId && m.IsPinned == true).CountAsync();

            return new Tuple<int, int>(pinnedRecords, totalRecords);
        }

        public async Task<bool> DeleteAllByParentId(int parentId)
        {
            try
            {
                var models = await _context.Uploads.Where(m => m.ParentId == parentId).ToListAsync();

                foreach (var model in models)
                {
                    _context.Uploads.Remove(model);
                }

                return await _context.SaveChangesAsync() > 0 ? true : false;

            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR({nameof(DeleteAllByParentId)}): {e.Message}");
            }

            return false;
        }

        public async Task<PagingResult<Upload>> SearchAllAsync(int pageIndex, int pageSize, string searchQuery)
        {
            var totalRecords = await _context.Uploads.CountAsync();
            var models = await _context.Uploads
                .Where(m => m.Name.Contains(searchQuery) || m.Title.Contains(searchQuery) || m.Title.Contains(searchQuery))
                .OrderByDescending(m => m.Id)
                //.Include(m => m.UploadsComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Upload>(models, totalRecords);
        }

        public async Task<PagingResult<Upload>> SearchAllByParentIdAsync(int pageIndex, int pageSize, string searchQuery, int parentId)
        {
            var totalRecords = await _context.Uploads.Where(m => m.ParentId == parentId)
                .Where(m => EF.Functions.Like(m.Name, $"%{searchQuery}%") || m.Title.Contains(searchQuery) || m.Title.Contains(searchQuery))
                .CountAsync();
            var models = await _context.Uploads.Where(m => m.ParentId == parentId)
                .Where(m => m.Name.Contains(searchQuery) || m.Title.Contains(searchQuery) || m.Title.Contains(searchQuery))
                .OrderByDescending(m => m.Id)
                //.Include(m => m.UploadsComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Upload>(models, totalRecords);
        }

        public async Task<SortedList<int, double>> GetMonthlyCreateCountAsync()
        {
            SortedList<int, double> createCounts = new SortedList<int, double>();

            // 1월부터 12월까지 0.0으로 초기화
            for (int i = 1; i <= 12; i++)
            {
                createCounts[i] = 0.0;
            }

            for (int i = 0; i < 12; i++)
            {
                // 현재 달부터 12개월 전까지 반복
                var current = DateTime.Now.AddMonths(-i);
                var cnt = _context.Uploads.AsEnumerable().Where(
                    m => m.Created != null
                    &&
                    Convert.ToDateTime(m.Created).Month == current.Month
                    &&
                    Convert.ToDateTime(m.Created).Year == current.Year
                ).ToList().Count();
                createCounts[current.Month] = cnt;
            }

            return await Task.FromResult(createCounts);
        }
    }
}
