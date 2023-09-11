using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizzyAPI.Models;
using QuizzyAPI.Models.Result;
using QuizzyAPI.Repositories;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using QuizzyAPI.Identity;

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
    public async Task<ActionResult<PaginatedDto<QuizBriefDto>>> GetQuizzes([FromQuery, Range(0, int.MaxValue)] int pageIndex = 0, [FromQuery, Range(1, MaxPageSize)] int pageSize = 10, [FromQuery] string? authorUsername = null) {
        var quizzes = await _quizzes.GetAll(pageIndex, pageSize, authorUsername);
        var quizzesCount = await _quizzes.GetCount(authorUsername);

        var pageCount = (int)Math.Ceiling((double)quizzesCount / (double)pageSize);

        // TODO: Fix URLs when running in docker
        // var prevUrl = pageIndex == 0
        //     ? null
        //     : Url.Link("GetQuizzes", new { pageIndex = pageIndex - 1, pageSize = pageSize });
        
        // var nextUrl = pageIndex == pageCount - 1
        //     ? null
        //     : Url.Link("GetQuizzes", new { pageIndex = pageIndex + 1, pageSize = pageSize });
        string? prevUrl = null;
        string? nextUrl = null;

        var response = new PaginatedDto<QuizBriefDto>(pageIndex, pageSize, pageCount, quizzesCount,
            _mapper.Map<ICollection<QuizBriefDto>>(quizzes), prevUrl, nextUrl);
        
        return Ok(response);
    }
    
    // GET: api/quizzes/{id}
    [HttpGet("{id:guid}", Name = "GetQuiz")]
    public async Task<ActionResult<QuizFullDto>> GetQuiz(Guid id) {
        var quiz = await _quizzes.GetById(id);

        if (quiz == null) {
            return NotFound();
        }

        return Ok(_mapper.Map<QuizFullDto>(quiz));
    }
    
    // DELETE: api/quizzes/{id}
    [HttpDelete("{id:guid}", Name = "DeleteQuiz")]
    [Authorize(Roles = Constants.Roles.ADMINISTRATOR)]
    public async Task<ActionResult<bool>> DeleteQuiz(Guid id) {
        // TODO: If Administrator OR owner
        var result = await _quizzes.DeleteQuiz(id);

        // TODO: Pretty message
        return result;
    }
    
    // PUT api/quizzes/{id}/result
    // Send answers and get a result
    [HttpPut("{id:guid}/result", Name = "GetResults")]
    public async Task<ActionResult<QuizResultDto>> GetResults(Guid id, AnswersRequestDto answers) {
        var quiz = await _quizzes.GetById(id);

        if (quiz == null) {
            return NotFound();
        }

        var answersCount = answers.Answers.Count;
        var expectedAnswersCount = quiz.Questions.Count();
        if (answersCount != expectedAnswersCount) {
            return BadRequest($"Invalid request. Expected {expectedAnswersCount} answers, found {answersCount}");
        }

        var results = await _quizzes.GetResults(id, answers);

        if (results == null) {
            return BadRequest("Invalid request.");
        }

        return Ok(results);
    }
}