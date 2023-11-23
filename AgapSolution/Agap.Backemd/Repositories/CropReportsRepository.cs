using Agap.Backemd.Data;
using Agap.Backemd.Helpers;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Repositories
{
    public class CropReportsRepository : GenericRepository<CropReport>, ICropReportsRepository
    {
        private readonly DataContext _context;

        public CropReportsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<CropReport>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.CropReports
                .Include(cropReport => cropReport.Crop)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(cropReport => cropReport.CropId.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new Response<IEnumerable<CropReport>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(cropReport => cropReport.CropId)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.CropReports.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(cropReport => cropReport.CropId.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new Response<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }
    }
}
