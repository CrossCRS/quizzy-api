namespace QuizzyAPI.Entities; 

public class Question {
    public int Id { get; set; }
    
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }
    
    public string Text { get; set; } = string.Empty;
    public int Points { get; set; }

    public IEnumerable<Answer> Answers { get; set; } = null!;
}