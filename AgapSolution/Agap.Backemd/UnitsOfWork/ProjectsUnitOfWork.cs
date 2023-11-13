using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class ProjectsUnitOfWork : GenericUnitOfWork<Project>, IProjectsUnitOfWork
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsUnitOfWork(IGenericRepository<Project> repository, IProjectsRepository projectsRepository) : base(repository)
        {
            _projectsRepository = projectsRepository;
        }

        public override async Task<Response<IEnumerable<Project>>> GetAsync(PaginationDTO pagination) => await _projectsRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _projectsRepository.GetTotalPagesAsync(pagination);
    }
}
