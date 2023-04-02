using AutoMapper;

namespace QuizzyAPI.Profiles; 

public class QuizProfile : Profile {
    public QuizProfile() {
        CreateMap<Entities.Quiz, Models.QuizBriefDto>();
    }
}