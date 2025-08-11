using Covoiturage_PFA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Covoiturage_PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeProfilsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DemandeProfilsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DemandeProfils
        [HttpGet("DemandeProfils")]
        public async Task<ActionResult<IEnumerable<DemandeProfil>>> GetDemandeProfils()
        {
            var profils = await _context.DemandeProfils
            .Select(p => new DemandeProfilDto
    {
                   username = p.username,
          
                  Nom = p.Nom,
                  Prenom = p.Prenom,
                  Email = p.Email,
                  Role = p.Role,
                  contact = p.contact,
                  Numdepermit = p.Numdepermit
            })
                .ToListAsync();

            return Ok(profils);

        }
    }
    public class DemandeProfilDto
    {
        public string username { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int contact { get; set; }
        public string Numdepermit { get; set; }
    }

}
