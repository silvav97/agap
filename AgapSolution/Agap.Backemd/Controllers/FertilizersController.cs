using Agap.Backemd.Data;
using Agap.Backemd.Interfaces;
using Agap.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agap.Backemd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FertilizersController : GenericController<Fertilizer>
    {
        public FertilizersController(IGenericUnitOfWork<Fertilizer> unitOfWork, DataContext context) : base(unitOfWork, context)

        {
        }
    }
}