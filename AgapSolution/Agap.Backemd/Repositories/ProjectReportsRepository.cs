using Agap.Backemd.Data;
using Agap.Backemd.Helpers;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Repositories
{
    public class ProjectReportsRepository : GenericRepository<ProjectReport>, IProjectReportsRepository
    {
        private readonly DataContext _context;

        public ProjectReportsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<ProjectReport>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.ProjectReports
                .Include(projectReport => projectReport.Project)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(projectReport => projectReport.Project.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new Response<IEnumerable<ProjectReport>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(projectReport => projectReport.ProjectId)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.ProjectReports.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(projectReport => projectReport.Project.Name.ToLower().Contains(pagination.Filter.ToLower()));
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
