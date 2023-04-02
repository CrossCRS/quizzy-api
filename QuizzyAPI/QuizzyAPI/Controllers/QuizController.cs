using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Data;
using QuizzyAPI.Entities;
using QuizzyAPI.Models;

namespace QuizzyAPI.Controllers;

[ApiController]
[Route("api/quizzes")]
public class QuizController : ControllerBase {
    private readonly QuizzyContext _context;
    private readonly IMapper _mapper;

    public QuizController(QuizzyContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/quizzes
    [HttpGet]
    public async Task<ActionResult<ICollection<Quiz>>> GetQuizzes() {
        var quizzes = await _context.Quizzes
            //.Include(q => q.Questions) // Show only in a single quiz endpoint
            //.ThenInclude(question => question.Answers)
            .OrderBy(q => q.Id)
            .ToListAsync();
        return Ok(_mapper.Map<ICollection<QuizBriefDto>>(quizzes));
    }
}