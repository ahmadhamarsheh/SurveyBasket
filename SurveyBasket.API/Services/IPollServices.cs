namespace SurveyBasket.API.Services
{
    public interface IPollServices
    {
        Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken);
        Task<Poll?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Poll> AddPollAsync(Poll request, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
