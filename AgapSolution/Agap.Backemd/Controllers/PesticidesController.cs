using Agap.Backemd.UnitsOfWork;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agap.Backemd.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PesticidesController : GenericController<Pesticide>
    {
        private readonly IPesticidesUnitOfWork _pesticidesUnitOfWork;

        public PesticidesController(IGenericUnitOfWork<Pesticide> unit, IPesticidesUnitOfWork pesticidesUnitOfWork) : base(unit)
        {
            _pesticidesUnitOfWork = pesticidesUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _pesticidesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _pesticidesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
