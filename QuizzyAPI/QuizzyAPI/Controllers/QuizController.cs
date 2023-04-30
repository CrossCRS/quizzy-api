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

    private const int MaxPageSize = 15;

    public QuizController(IQuizRepository quizzes, IMapper mapper) {
        _quizzes = quizzes;
        _mapper = mapper;
    }

    // GET: api/quizzes
    [HttpGet(Name = "GetQuizzes")]
    public async Task<ActionResult<PaginatedDto<QuizBriefDto>>> GetQuizzes([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10) {
        var quizzes = await _quizzes.GetAll(pageIndex, pageSize);
        var quizzesCount = await _quizzes.GetCount();

        if (pageSize > MaxPageSize) {
            pageSize = MaxPageSize;
        }

        var pageCount = (int)Math.Ceiling((double)quizzesCount / (double)pageSize);

        var prevUrl = pageIndex == 0
            ? null
            : Url.Link("GetQuizzes", new { pageIndex = pageIndex - 1, pageSize = pageSize });
        
        var nextUrl = pageIndex == pageCount - 1
            ? null
            : Url.Link("GetQuizzes", new { pageIndex = pageIndex + 1, pageSize = pageSize });

        var response = new PaginatedDto<QuizBriefDto>(pageIndex, pageSize, pageCount, quizzesCount,
            _mapper.Map<ICollection<QuizBriefDto>>(quizzes), prevUrl, nextUrl);
        
        return Ok(response);
    }
    
    // GET: api/quizzes/1
    [HttpGet("{id:int}", Name = "GetQuiz")]
    public async Task<ActionResult<QuizFullDto>> GetQuiz(int id) {
        var quiz = await _quizzes.GetById(id);

        if (quiz == null) {
            return NotFound();
        }

        return Ok(_mapper.Map<QuizFullDto>(quiz));
    }
    
    // PUT api/quizzes/1/result
    // Send answers and get a result
    [HttpPut("{id:int}/result", Name = "GetResults")]
    public async Task<ActionResult<QuizResultDto>> GetResults(int id, AnswersRequestDto answers) {
        var results = await _quizzes.GetResults(id, answers);
        return Ok(results);
    }
}