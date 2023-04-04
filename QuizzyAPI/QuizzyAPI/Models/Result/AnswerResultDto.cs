namespace QuizzyAPI.Models.Result; 

public class AnswerResultDto {
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public bool IsChosen { get; set; }
}