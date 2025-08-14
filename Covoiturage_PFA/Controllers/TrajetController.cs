using Microsoft.AspNetCore.Mvc;
using Covoiturage_PFA.Models;
using Microsoft.EntityFrameworkCore;


namespace Covoiturage_PFA.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TrajetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrajetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Trajet trajet)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // (Optionnel) vérifier que le ConducteurId existe dans AspNetUsers
            // var exists = await _context.Users.AnyAsync(u => u.Id == trajet.ConducteurId);
            // if (!exists) return BadRequest("Conducteur inexistant.");

            _context.Trajets.Add(trajet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = trajet.Id }, trajet);
        }

        // GET /api/trajet/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var t = await _context.Trajets.FindAsync(id);
            return t is null ? NotFound() : Ok(t);
        }

        // GET /api/trajet?conducteurId=xxxx
        [HttpGet("gettrajet")]
        public async Task<IActionResult> GetAll([FromQuery] string? conducteurId)
        {
            //IQueryable<Trajet> q = _context.Trajets.AsNoTracking().OrderByDescending(t => t.Id);
            //if (!string.IsNullOrWhiteSpace(conducteurId))
            //    q = q.Where(t => t.ConducteurId == conducteurId);

            var list = await _context.Trajets
            .AsNoTracking()
            .Where(t => t.ConducteurId == conducteurId)
            .OrderByDescending(t => t.Id)
            .ToListAsync();
            return Ok(list);
        }
    }
}