using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizzyAPI.Identity;
using QuizzyAPI.Models;
using QuizzyAPI.Repositories;

namespace QuizzyAPI.Controllers; 

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase {
    private readonly UserManager<QuizzyUser> _userManager;
    private readonly RoleManager<QuizzyRole> _roleManager;
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _mapper;

    public UserController(UserManager<QuizzyUser> userManager, RoleManager<QuizzyRole> roleManager, IQuizRepository quizRepository, IMapper mapper) {
        _userManager = userManager;
        _roleManager = roleManager;
        _quizRepository = quizRepository;
        _mapper = mapper;
    }
    
    // GET: api/users/{id}
    [HttpGet("{id:int}", Name = "GetUser")]
    public async Task<ActionResult<UserFullDto>> GetUser([Range(1, int.MaxValue)] int id) {
        // TODO: Move to user service?
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null) {
            return NotFound();
        }

        var userDto = _mapper.Map<UserFullDto>(user);
        // TODO: AutoMapper for quiz counts
        userDto.CreatedQuizzesCount = await _quizRepository.GetCount(user.Id);

        return Ok(userDto);
    }
}