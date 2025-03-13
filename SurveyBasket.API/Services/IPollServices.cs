

namespace SurveyBasket.API.Services
{
    public interface IPollServices
    {
        Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken);
        Task<OneOf<PollResponse, Error>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<PollResponse>> AddPollAsync(PollRequest request, CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int id, PollRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
