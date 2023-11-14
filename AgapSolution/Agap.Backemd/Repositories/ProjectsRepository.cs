using Agap.Backemd.Data;
using Agap.Backemd.Helpers;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Repositories
{
    public class ProjectsRepository : GenericRepository<Project>, IProjectsRepository
    {
        private readonly DataContext _context;

        public ProjectsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<Project>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Projects
                .Include(project => project.CropType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(project => project.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new Response<IEnumerable<Project>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(project => project.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Projects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(project => project.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new Response<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public override async Task<Response<Project>> GetAsync(int id)
        {
            var project = await _context.Projects
                 .Include(project => project.CropList!)
                 //.ThenInclude(crop => crop.ExpenseList)
                 .FirstOrDefaultAsync(project => project.Id == id);

            if (project == null)
            {
                return new Response<Project>
                {
                    WasSuccess = false,
                    Message = "Proyecto no existe"
                };
            }

            return new Response<Project>
            {
                WasSuccess = true,
                Result = project
            };
        }
    }
}
