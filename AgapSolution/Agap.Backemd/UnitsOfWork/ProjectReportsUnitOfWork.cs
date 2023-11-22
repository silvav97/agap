using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class ProjectReportsUnitOfWork : GenericUnitOfWork<ProjectReport>, IProjectReportsUnitOfWork
    {
        private readonly IProjectReportsRepository _projectReportsRepository;

        public ProjectReportsUnitOfWork(IGenericRepository<ProjectReport> repository, IProjectReportsRepository projectReportsRepository) : base(repository)
        {
            _projectReportsRepository = projectReportsRepository;
        }

        public override async Task<Response<IEnumerable<ProjectReport>>> GetAsync(PaginationDTO pagination) => await _projectReportsRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _projectReportsRepository.GetTotalPagesAsync(pagination);
    }
}
