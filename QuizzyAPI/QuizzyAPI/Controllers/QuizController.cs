using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizzyAPI.Models;
using QuizzyAPI.Models.Result;
using QuizzyAPI.Repositories;

namespace QuizzyAPI.Controllers;

[ApiController]
[Route("api/quizzes")]
public class QuizController : ControllerBase {
    private readonly IQuizRepository _quizzes;
    private readonly IMapper _mapper;

    public QuizController(IQuizRepository quizzes, IMapper mapper) {
        _quizzes = quizzes;
        _mapper = mapper;
    }

    // GET: api/quizzes
    [HttpGet]
    public async Task<ActionResult<ICollection<QuizBriefDto>>> GetQuizzes() {
        // TODO: Pagination
        var quizzes = await _quizzes.GetAll();
        return Ok(_mapper.Map<ICollection<QuizBriefDto>>(quizzes));
    }
    
    // GET: api/quizzes/1
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizFullDto>> GetQuiz(int id) {
        var quiz = await _quizzes.GetById(id);

        if (quiz == null) {
            return NotFound();
        }

        return Ok(_mapper.Map<QuizFullDto>(quiz));
    }
    
    // PUT api/quizzes/1/result
    // Send answers and get a result
    [HttpPut("{id}")]
    public async Task<ActionResult<QuizResultDto>> GetResults(int id, AnswersRequestDto answers) {
        return Ok(null);
    }
}