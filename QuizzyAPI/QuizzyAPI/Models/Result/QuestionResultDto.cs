namespace QuizzyAPI.Models.Result; 

public class QuestionResultDto {
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;
    public int Points { get; set; }

    public IEnumerable<AnswerResultDto> Answers { get; set; } = null!;
}