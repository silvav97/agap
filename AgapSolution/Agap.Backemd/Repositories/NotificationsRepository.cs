using Agap.Backemd.Data;
using Agap.Backemd.Helpers;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Repositories
{
    public class NotificationsRepository : GenericRepository<Notification>, INotificationsRepository
    {
        private readonly DataContext _context;

        public NotificationsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<Notification>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Notifications
                .Include(notification => notification.Crop)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.TitleMessage.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new Response<IEnumerable<Notification>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.TitleMessage)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Notifications.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.TitleMessage.ToLower().Contains(pagination.Filter.ToLower()));
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
