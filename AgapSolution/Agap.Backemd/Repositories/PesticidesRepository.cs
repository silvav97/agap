using Agap.Backemd.Data;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Helpers;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Repositories
{
    public class PesticidesRepository : GenericRepository<Pesticide>, IPesticidesRepository
    {
        private readonly DataContext _context;

        public PesticidesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<Pesticide>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Pesticides.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new Response<IEnumerable<Pesticide>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Pesticides.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
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
