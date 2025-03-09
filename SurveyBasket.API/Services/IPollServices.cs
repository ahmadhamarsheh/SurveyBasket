

namespace SurveyBasket.API.Services
{
    public interface IPollServices
    {
        Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken);
        Task<OneOf<PollResponse, Error>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<OneOf<PollResponse, Error>> AddPollAsync(PollRequest request, CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int id, PollRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
