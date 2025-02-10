namespace SurveyBasket.API.Services
{
    public interface IPollServices
    {
        IEnumerable<Poll> GetAll();
        Poll? GetById(int id);
        Poll Add(Poll request);
        bool Update(int id, Poll poll);
        bool Delete(int id);
    }
}
