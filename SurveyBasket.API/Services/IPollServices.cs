namespace SurveyBasket.API.Services
{
    public interface IPollServices
    {
        Task<IEnumerable<Poll>> GetAllAsync();
        Task<Poll?> GetByIdAsync(int id);
        Task<Poll> AddPollAsync(Poll request);
        //bool Update(int id, Poll poll);
        //bool Delete(int id);
    }
}
