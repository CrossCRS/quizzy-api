namespace QuizzyAPI.Entities; 

public class Quiz {
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public IEnumerable<Question> Questions { get; set; } = null!;

    public bool HideAnswers { get; set; }
}