using QuizzyAPI.Entities;

namespace QuizzyAPI.Repositories; 

public interface IQuizRepository {
    Task<Quiz?> GetById(int id);
    Task<IEnumerable<Quiz>> GetAll();
}