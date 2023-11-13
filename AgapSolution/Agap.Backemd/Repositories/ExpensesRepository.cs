using Agap.Backemd.Data;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Helpers;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Repositories
{
    public class ExpensesRepository : GenericRepository<Expense>, IExpensesRepository
    {
        private readonly DataContext _context;

        public ExpensesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Response<IEnumerable<Expense>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Expenses
                .Include(expense => expense.Crop)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(expense => expense.ExpenseDescription.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new Response<IEnumerable<Expense>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(expense => expense.ExpenseDescription)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Expenses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(expense => expense.ExpenseDescription.ToString().ToLower().Contains(pagination.Filter.ToLower()));
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
