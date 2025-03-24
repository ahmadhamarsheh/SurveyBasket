using SurveyBasket.API.Contract.Questions;

namespace SurveyBasket.API.Services
{
    public interface IQuestionServices
    {
        Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken = default);
        Task<Result<QuestionResponse>> GetAsync(int pollId, int Id, CancellationToken cancellationToken = default);
        Task<Result<QuestionResponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellationToken = default); 
    }
}
