using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Data;
using QuizzyAPI.Entities;
using QuizzyAPI.Models;
using QuizzyAPI.Models.Result;

namespace QuizzyAPI.Repositories; 

public class QuizRepository : IQuizRepository {
    private readonly QuizzyContext _context;

    public QuizRepository(QuizzyContext context) {
        _context = context;
    }

    public async Task<Quiz?> GetById(int id) {
        return await _context.Quizzes
            .Include(q => q.Author)
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .Where(q => q.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Quiz>> GetAll(int pageIndex, int pageSize, int? authorId = null) {
        IQueryable<Quiz> query = _context.Quizzes;

        if (authorId != null) {
            query = query.Where(q => q.AuthorId == authorId);
        }

        query = query
            .Include(q => q.Author)
            .Include(q => q.Questions)
            .OrderBy(q => q.Id)
            .Skip(pageIndex * pageSize)
            .Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<long> GetCount(int? authorId = null) {
        IQueryable<Quiz> query = _context.Quizzes;

        if (authorId != null) {
            query = query.Where(q => q.AuthorId == authorId);
        }

        return await query.LongCountAsync();
    }

    public async Task<QuizResultDto?> GetResults(int id, AnswersRequestDto request) {
        var quiz = await GetById(id);

        if (quiz == null) {
            return null;
        }

        return await Task.Run(() => {
            var response = new QuizResultDto {
                Id = id,
                Title = quiz.Title,
                Description = quiz.Description,
                PointsMax = 0,
                PointsScored = 0,
                Questions = new List<QuestionResultDto>()
            };

            // Questions
            foreach (var question in quiz.Questions) {
                response.PointsMax += question.Points;

                var questionResultDto = new QuestionResultDto {
                    Id = question.Id,
                    Points = question.Points,
                    Text = question.Text,
                    Answers = new List<AnswerResultDto>()
                };
                
                // Answers
                foreach (var answer in question.Answers) {
                    if (!request.Answers.ContainsKey(question.Id.ToString())) {
                        return null;
                    }

                    var answerResultDto = new AnswerResultDto {
                        Id = answer.Id,
                        Text = answer.Text,
                        IsCorrect = answer.IsCorrect,
                        IsChosen = (request.Answers[question.Id.ToString()] == answer.Id)
                    };

                    if (answerResultDto.IsChosen && answerResultDto.IsCorrect) {
                        response.PointsScored += 1;
                        // TODO: Negative points?
                    }
                    
                    questionResultDto.Answers.Add(answerResultDto);
                }

                response.Questions.Add(questionResultDto);
            }

            return response;
        });
    }
}