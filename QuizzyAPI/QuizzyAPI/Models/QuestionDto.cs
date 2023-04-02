namespace QuizzyAPI.Models; 

public class QuestionDto {
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;
    public int Points { get; set; }

    public IEnumerable<AnswerDto> Answers { get; set; } = null!;
}