using QuizzyAPI.Entities;
using QuizzyAPI.Models;
using QuizzyAPI.Models.Result;

namespace QuizzyAPI.Repositories; 

public interface IQuizRepository {
    Task<Quiz?> GetById(int id);
    Task<IEnumerable<Quiz>> GetAll();

    Task<QuizResultDto?> GetResults(int id, AnswersRequestDto answers);
}