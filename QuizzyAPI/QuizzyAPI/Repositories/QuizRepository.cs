using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Data;
using QuizzyAPI.Entities;

namespace QuizzyAPI.Repositories; 

public class QuizRepository : IQuizRepository {
    private readonly QuizzyContext _context;

    public QuizRepository(QuizzyContext context) {
        _context = context;
    }

    public async Task<Quiz?> GetById(int id) {
        return await _context.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .Where(q => q.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Quiz>> GetAll() {
        return await _context.Quizzes
            .OrderBy(q => q.Id)
            .ToListAsync();
    }
}