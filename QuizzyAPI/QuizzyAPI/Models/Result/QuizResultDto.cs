namespace QuizzyAPI.Models.Result; 

public class QuizResultDto {
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public int PointsScores { get; set; }
    public int PointsMax { get; set; }

    public IEnumerable<QuestionResultDto> Questions { get; set; } = null!;
}