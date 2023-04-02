namespace QuizzyAPI.Models; 

public class QuizBriefDto {
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool HideAnswers { get; set; }
}