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
                queryable = queryable.Where(cropReport => cropReport.Crop.Name.ToLower().Contains(pagination.Filter.ToLower()));
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
                queryable = queryable.Where(cropReport => cropReport.Crop.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new Response<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }


        public async Task<Response<int>> AddAsync(int cropId)
        {
            try
            {
                // Aquí asumimos que hay lógica para obtener los datos necesarios del cultivo
                var crop = await _context.Crops.FirstOrDefaultAsync(crop => crop.Id == cropId);
                if (crop == null)
                {
                    return new Response<int> { WasSuccess = false, Message = "Cultivo no encontrado." };
                }

                float totalSale = 54321.5F;
                float realExpense = 12345.2F;

                var cropReport = new CropReport
                {
                    CropId = cropId,
                    Crop = crop,
                    TotalSale = totalSale,
                    AssignedBudget = crop.AssignedBudget,
                    ExpectedExpense = crop.ExpectedExpense, 
                    RealExpense = realExpense,
                    Profit = totalSale - realExpense,
                    Profitability = (totalSale - realExpense) / crop.AssignedBudget
                };

                _context.CropReports.Add(cropReport);
                await _context.SaveChangesAsync();

                return new Response<int> { WasSuccess = true, Result = cropReport.Id };
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta con error
                return new Response<int> { WasSuccess = false, Message = ex.Message };
            }
        }
    }
}
