using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Identity;

namespace QuizzyAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase {
    private readonly UserManager<QuizzyUser> _userManager;
    private readonly RoleManager<QuizzyRole> _roleManager;

    public AuthenticationController(UserManager<QuizzyUser> userManager, RoleManager<QuizzyRole> roleManager) {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET: api/auth/roles
    //[Authorize(Roles = Constants.Roles.ADMINISTRATOR)] // TODO: Uncomment later :)
    [HttpGet("roles", Name = "GetRoles")]
    public async Task<ActionResult<IEnumerable<QuizzyRole>>> GetRoles() {
        var response = await _roleManager.Roles.ToListAsync();

        return Ok(response);
    }

    // GET: api/auth/users
    //[Authorize(Roles = Constants.Roles.ADMINISTRATOR)]
    [HttpGet("users", Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<QuizzyUser>>> GetUsers() {
        var response = await _userManager.Users.ToListAsync();

        return Ok(response);
    }
}