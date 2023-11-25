using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class NotificationsUnitOfWork : GenericUnitOfWork<Notification>, INotificationsUnitOfWork
    {
        private readonly INotificationsRepository _notificationsRepository;

        public NotificationsUnitOfWork(IGenericRepository<Notification> repository, INotificationsRepository notificationsRepository) : base(repository)
        {
            _notificationsRepository = notificationsRepository;
        }

        public override async Task<Response<IEnumerable<Notification>>> GetAsync(PaginationDTO pagination) => await _notificationsRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _notificationsRepository.GetTotalPagesAsync(pagination);
    }
}
