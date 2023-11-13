using Agap.Backemd.Data;
using Agap.Backemd.Helpers;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Repositories
{
    public class CropsRepository : GenericRepository<Crop>, ICropsRepository
    {
        private readonly DataContext _context;

        public CropsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<Crop>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Crops
                .Include(crop => crop.Project)
                .Include(crop => crop.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(crop => crop.Status.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new Response<IEnumerable<Crop>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(crop => crop.Status)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Crops.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(crop => crop.Status.ToString().ToLower().Contains(pagination.Filter.ToLower()));
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
