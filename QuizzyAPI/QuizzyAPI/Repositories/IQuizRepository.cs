using QuizzyAPI.Entities;
using QuizzyAPI.Models;
using QuizzyAPI.Models.Result;

namespace QuizzyAPI.Repositories; 

public interface IQuizRepository {
    Task<Quiz?> GetById(Guid id);
    Task<IEnumerable<Quiz>> GetAll(int pageIndex, int pageSize, Guid? authorId = null);
    Task<long> GetCount(Guid? authorId = null);

    Task<bool> DeleteQuiz(Guid id);

    Task<QuizResultDto?> GetResults(Guid id, AnswersRequestDto answers);
}