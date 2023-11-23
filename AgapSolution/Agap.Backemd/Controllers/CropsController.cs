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
    public class CropsController : GenericController<Crop>
    {
        private readonly ICropsUnitOfWork _cropsUnitOfWork;

        public CropsController(IGenericUnitOfWork<Crop> unit, ICropsUnitOfWork cropsUnitOfWork) : base(unit)
        {
            _cropsUnitOfWork = cropsUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _cropsUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("all")]
        public override async Task<IActionResult> GetAllAsync()
        {
            var response = await _cropsUnitOfWork.GetAllAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _cropsUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }


        [HttpPut("closecrop/{cropId}")]
        public async Task<IActionResult> CloseCropAsync(int cropId)
        {
            if (cropId <= 0)
            {
                return BadRequest("Invalid crop ID");
            }

            var action = await _cropsUnitOfWork.CloseCropAsync(cropId);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

    }
}
