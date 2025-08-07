using Covoiturage_PFA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Covoiturage_PFA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class AuthController : ControllerBase
        {
        //private readonly UserManager<IdentityUser> _userManager;
        UserManager<ApplicationUser> _userManager;
       private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;
         

            public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                 _context = context;
             }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            }

            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null) {
                return BadRequest("Utilisateur existe déja!"); }

            var user = new ApplicationUser
            {
                UserName = model.username,
                Email = model.Email,
                Nom = model.Nom,
                Role = model.Role,
                Prenom = model.Prenom,
                //PhoneNumber = model.contact,
                Numpermit = model.Numpermit
            };
           

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
          

            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok("Demande envoiyé avec succès!");
        }

        [HttpGet("roles")]
            public IActionResult GetRoles()
            {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            ////var roles = _roleManager.Roles.ToList();

            return Ok(roles);
            }

        [HttpPost("DemandeProfil")]
        public async Task<IActionResult> DemandeProfil([FromBody] RegisterDto model)
        {
            try { 
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

            }

            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest("Utilisateur existe déja!");
            }


            var DemandeProfil = new DemandeProfil
            {
                username = model.username,
                Email = model.Email,
                Nom = model.Nom,
                Role = model.Role,
                Prenom = model.Prenom,
                contact = model.contact,
                Numdepermit = model.Numpermit,
                StatusId = 1
            };

            _context.Add(DemandeProfil);
            _context.SaveChanges();
            return Ok("Demande envoyé avec succès!");

            }
            catch(Exception e)
            {
                return BadRequest(e);

            }
        }
    }

    }


public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public int contact { get; set; }
    public string Numpermit { get; set; }


    public string username { get; set; }


}

