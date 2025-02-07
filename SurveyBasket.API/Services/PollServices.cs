
using Microsoft.AspNetCore.Http.HttpResults;

namespace SurveyBasket.API.Services
{
    public class PollServices : IPollServices
    {
        private readonly List<Poll> _polls = new List<Poll>
        {
            new Poll
            {
                Id = 1,
                Title = "Test",
                Description = "Test Test",
            }
        };
        public IEnumerable<Poll> GetAll() => _polls;

        public Poll? GetById(int id) => _polls.SingleOrDefault(pol => pol.Id == id);

    }
}
