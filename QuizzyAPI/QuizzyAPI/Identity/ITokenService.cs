namespace QuizzyAPI.Identity; 

public interface ITokenService {
    Task<string> GetTokenAsync(string userName);
}
