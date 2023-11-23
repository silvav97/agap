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
                .Where(crop => crop.ProjectId == pagination.Id)
                .AsQueryable();
            

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(crop => crop.Name.ToLower().Contains(pagination.Filter.ToLower()));
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

        public override async Task<Response<IEnumerable<Crop>>> GetAllAsync()
        {
            var queryable = _context.Crops
            .Include(crop => crop.Project)
            .Include(crop => crop.User)
            .AsQueryable();

            return new Response<IEnumerable<Crop>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(crop => crop.Status)
                    .ToListAsync()
            };
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Crops.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(crop => crop.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new Response<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }


        public async Task<Response<bool>> CloseCropAsync(int cropId)
        {
            var crop = await _context.Crops.FirstOrDefaultAsync(c => c.Id == cropId);
            if (crop == null)
            {
                return new Response<bool>
                {
                    WasSuccess = false,
                    Message = "Cultivo no encontrado."
                };
            }

            crop.Status = CropStatus.Cerrado;
            _context.Crops.Update(crop);
            await _context.SaveChangesAsync();

            return new Response<bool>
            {
                WasSuccess = true,
                Result = true
            };
        }


    }
}