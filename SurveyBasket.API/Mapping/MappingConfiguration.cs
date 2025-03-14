using SurveyBasket.API.Contract.Polls;
using SurveyBasket.API.Contract.Questions;

namespace SurveyBasket.API.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Poll, PollResponse>()
                .Map(dest => dest.Summary, src => src.Summary);

            config.NewConfig<QuestionRequest, Question>()
                .Map(dest => dest.Answers, src => src.Answers.Select(answer => new Answer { Content = answer }));
        }
    }
}
