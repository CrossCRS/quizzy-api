namespace QuizzyAPI.Models;

public class UserFullDto {
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    
    public long CreatedQuizzesCount { get; set; }
}