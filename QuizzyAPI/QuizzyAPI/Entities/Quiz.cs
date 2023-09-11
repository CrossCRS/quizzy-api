using QuizzyAPI.Identity;

namespace QuizzyAPI.Entities; 

public class Quiz {
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public QuizzyUser Author { get; set; } = null!;

    public IEnumerable<Question> Questions { get; set; } = null!;

    public bool HideAnswers { get; set; }
}