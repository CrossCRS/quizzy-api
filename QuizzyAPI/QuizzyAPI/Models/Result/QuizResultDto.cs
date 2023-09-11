namespace QuizzyAPI.Models.Result; 

public class QuizResultDto {
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public int PointsScored { get; set; }
    public int PointsMax { get; set; }

    public ICollection<QuestionResultDto> Questions { get; set; } = null!;
}