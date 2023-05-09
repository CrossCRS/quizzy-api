namespace QuizzyAPI.Models.Identity;

public class LoginResponseDto {
    public bool Result { get; set; } = false;
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}
