using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;

namespace Agap.Backemd.UnitsOfWork
{
    public class ExpensesUnitOfWork : GenericUnitOfWork<Expense>, IExpensesUnitOfWork
    {
        private readonly IExpensesRepository _expensesRepository;

        public ExpensesUnitOfWork(IGenericRepository<Expense> repository, IExpensesRepository expensesRepository) : base(repository)
        {
            _expensesRepository = expensesRepository;
        }

        public override async Task<Response<IEnumerable<Expense>>> GetAsync(PaginationDTO pagination) => await _expensesRepository.GetAsync(pagination);

        public override async Task<Response<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _expensesRepository.GetTotalPagesAsync(pagination);
    }
}
