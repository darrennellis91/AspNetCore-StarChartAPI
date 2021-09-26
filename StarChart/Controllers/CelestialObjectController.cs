using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext _context { get; }
    }
}
