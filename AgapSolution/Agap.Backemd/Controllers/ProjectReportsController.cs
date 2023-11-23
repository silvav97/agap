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
    public class ProjectReportsController : GenericController<ProjectReport>
    {
        private readonly IProjectReportsUnitOfWork _projectReportsUnitOfWork;

        public ProjectReportsController(IGenericUnitOfWork<ProjectReport> unit, IProjectReportsUnitOfWork projectReportsUnitOfWork) : base(unit)
        {
            _projectReportsUnitOfWork = projectReportsUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _projectReportsUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _projectReportsUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
