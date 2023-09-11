namespace QuizzyAPI.Models;

public class QuizBriefDto {
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public UserBriefDto Author { get; set; } = null!;

    public int QuestionsCount { get; set; }
}
