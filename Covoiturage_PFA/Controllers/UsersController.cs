using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Covoiturage_PFA.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
  

    // GET api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserById(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.Id,
            user.Nom,
            user.Prenom,
            user.UserName,
            user.Email,
            user.PhoneNumber,
            user.Role,
            user.Numpermit,
            user.StatusId
        });
    }
}
