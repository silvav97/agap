using Agap.Backemd.Interfaces;
using Agap.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agap.Backemd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        public CitiesController(IGenericUnitOfWork<City> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
