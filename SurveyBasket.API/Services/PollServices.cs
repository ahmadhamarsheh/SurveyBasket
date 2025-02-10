
using Microsoft.AspNetCore.Http.HttpResults;

namespace SurveyBasket.API.Services
{
    public class PollServices : IPollServices
    {
        private static readonly List<Poll> _polls = new List<Poll>
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

        public Poll Add(Poll request)
        {
            request.Id = _polls.Count + 1;
            _polls.Add(request);
            return request;
        }

        public bool Update(int id, Poll poll)
        {
            var curPoll = GetById(id);
            if (curPoll == null)
            {
                return false;
            }
            curPoll.Title = poll.Title;
            curPoll.Description = poll.Description;
            return true;
        }

        public bool Delete(int id)
        {
            var poll = GetById(id);
            if(poll == null)
            {
                return false;
            }
            _polls.Remove(poll);
            return true;
        }
    }
}
