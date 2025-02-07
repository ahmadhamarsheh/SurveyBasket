namespace SurveyBasket.API.Services
{
    public interface IPollServices
    {
        IEnumerable<Poll> GetAll();
        Poll? GetById(int id);
    }
}
