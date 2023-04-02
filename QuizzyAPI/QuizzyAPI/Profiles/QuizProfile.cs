using AutoMapper;

namespace QuizzyAPI.Profiles; 

public class QuizProfile : Profile {
    public QuizProfile() {
        CreateMap<Entities.Quiz, Models.QuizBriefDto>();
        CreateMap<Entities.Quiz, Models.QuizFullDto>();

        CreateMap<Entities.Question, Models.QuestionDto>();

        CreateMap<Entities.Answer, Models.AnswerDto>();
    }
}