namespace QuizzyAPI.Models; 

// A request containing selected answers
public class AnswersRequestDto {
    public Dictionary<string, int> Answers { get; set; } = null!;
}