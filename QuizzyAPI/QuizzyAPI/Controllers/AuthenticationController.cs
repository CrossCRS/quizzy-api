using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Identity;
using QuizzyAPI.Models.Identity;

namespace QuizzyAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase {
    private readonly SignInManager<QuizzyUser> _signInManager;
    private readonly UserManager<QuizzyUser> _userManager;
    private readonly RoleManager<QuizzyRole> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthenticationController(SignInManager<QuizzyUser> signInManager, UserManager<QuizzyUser> userManager, RoleManager<QuizzyRole> roleManager, ITokenService tokenService) {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    // POST: api/auth/login
    [HttpPost("login", Name = "Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginRequestDto request) {
        var response = new LoginResponseDto();

        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

        response.Result = result.Succeeded;
        response.Username = request.Username;

        if (result.Succeeded) {
            response.Token = await _tokenService.GetTokenAsync(request.Username);
        }

        return Ok(response);
    }

    // GET: api/auth/roles
    [Authorize(Roles = Constants.Roles.ADMINISTRATOR)]
    [HttpGet("roles", Name = "GetRoles")]
    public async Task<ActionResult<IEnumerable<QuizzyRole>>> GetRoles() {
        var response = await _roleManager.Roles.ToListAsync();

        return Ok(response);
    }

    // GET: api/auth/users
    [Authorize(Roles = Constants.Roles.ADMINISTRATOR)]
    [HttpGet("users", Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<QuizzyUser>>> GetUsers() {
        var response = await _userManager.Users.ToListAsync();

        return Ok(response);
    }
}