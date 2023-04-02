using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Data;
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
    public async Task<ActionResult<ICollection<QuizBriefDto>>> GetQuizzes() {
        var quizzes = await _context.Quizzes
            .OrderBy(q => q.Id)
            .ToListAsync();
        return Ok(_mapper.Map<ICollection<QuizBriefDto>>(quizzes));
    }
    
    // GET: api/quizzes/1
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizFullDto>> GetQuiz(int id) {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .Where(q => q.Id == id)
            .FirstOrDefaultAsync();

        if (quiz == null) {
            return NotFound();
        }

        return Ok(_mapper.Map<QuizFullDto>(quiz));
    }
}