using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Data;
using QuizzyAPI.Entities;

namespace QuizzyAPI.Controllers;

[ApiController]
[Route("api/quizzes")]
public class QuizController : ControllerBase {
    private readonly QuizzyContext _context;

    public QuizController(QuizzyContext context) {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Quiz>>> GetQuizzes() {
        var quizzes = await _context.Quizzes
            //.Include(q => q.Questions) // Show only in a single quiz endpoint
            //.ThenInclude(question => question.Answers)
            .OrderBy(q => q.Id)
            .ToListAsync();
        return Ok(quizzes);
    }
}