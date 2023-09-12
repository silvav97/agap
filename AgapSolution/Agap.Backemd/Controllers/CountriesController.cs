using Agap.Backemd.Data;
using Agap.Backemd.Interfaces;
using Agap.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>
    {
        private readonly DataContext _context;

        public CountriesController(IGenericUnitOfWork<Country> unitOfWork, DataContext context) : base(unitOfWork)
        {
            _context = context;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Countries
                .Include(c => c.States)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var country = await _context.Countries
                .Include(c => c.States!)
                .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

    }
}
