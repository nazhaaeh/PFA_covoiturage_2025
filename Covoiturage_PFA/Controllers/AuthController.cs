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

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterDto model)
        //{
        //    try { 
        //    if (!ModelState.IsValid)
        //    {
        //        //return BadRequest(ModelState);
        //        return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

        //    }

        //    var userExists = await _userManager.FindByEmailAsync(model.Email);
        //    if (userExists != null) {
        //        return BadRequest("Utilisateur existe déja!"); }

        //    var user = new ApplicationUser
        //    {
        //        UserName = model.username,
        //        Email = model.Email,
        //        Nom = model.Nom,
        //        Role = model.Role,
        //        Prenom = model.Prenom,
        //        PhoneNumber = model.contact,
        //        Numpermit = model.Numpermit,
        //        StatusId = 1

        //    };


        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (!result.Succeeded)s
        //    {
        //        return BadRequest(result.Errors);
        //    }


        //    await _userManager.AddToRoleAsync(user, model.Role);

        //    return Ok("Demande envoyé avec succès!");
        //}
        //     catch(Exception e)
        //    {
        //        return BadRequest(e);

        //    }
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }

                // Vérifier si email existe déjà
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    return BadRequest("Un utilisateur avec cet email existe déjà !");
                }

                // Vérifier si username existe déjà
                var userByUsername = await _userManager.FindByNameAsync(model.username);
                if (userByUsername != null)
                {
                    return BadRequest("Un utilisateur avec ce nom d'utilisateur existe déjà !");
                }

                var user = new ApplicationUser
                {
                    UserName = model.username,
                    Email = model.Email,
                    Nom = model.Nom,
                    Role = model.Role,
                    Prenom = model.Prenom,
                    PhoneNumber = model.contact,
                    Numpermit = model.Numpermit,
                    StatusId = 1
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                await _userManager.AddToRoleAsync(user, model.Role);

                return Ok("Demande envoyée avec succès !");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Données invalides");

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Email ou mot de passe incorrect");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
                return Unauthorized("Email ou mot de passe incorrect");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                message = "Connexion réussie",
                userId = user.Id,
                username = user.UserName,
                email = user.Email,
                nom = user.Nom,
                prenom = user.Prenom,
                role = roles.FirstOrDefault()

            });
        }
        [HttpGet("Getdemandeprofil")]
        public IActionResult Getdemandeprofil()
        {
            var demande = _context.Users
                .Where(u => u.StatusId == 1) 
                .ToList(); 

            return Ok(demande);
        }


        //[HttpPost("UpdateDemandeStatus")]
        //public IActionResult UpdateDemandeStatus([FromBody] string userId, [FromBody] int statusId)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        //    if (user == null)
        //    {
        //        return NotFound("Utilisateur introuvable");
        //    }

        //    user.StatusId = statusId; 
        //    _context.SaveChanges();

        //    return Ok(new { message = "Statut mis à jour avec succès" });
        //}

        [HttpPost("UpdateDemandeStatus")]
        public IActionResult UpdateDemandeStatus([FromBody] UpdateDemandeStatusRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.UserId);
            if (user == null)
            {
                return NotFound("Utilisateur introuvable");
            }

            user.StatusId = request.StatusId;
            _context.SaveChanges();

            return Ok($"Statut de la demande mis à jour à {request.StatusId}");
        }

        public class UpdateDemandeStatusRequest
        {
            public string UserId { get; set; }
            public int StatusId { get; set; }
        }
    }



}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }

}
public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string contact { get; set; }
    public string Numpermit { get; set; }


    public string username { get; set; }


}

