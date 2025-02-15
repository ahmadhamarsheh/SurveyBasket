
using Microsoft.AspNetCore.Http.HttpResults;

namespace SurveyBasket.API.Services
{
    public class PollServices(ApplicationDbContext context) : IPollServices
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Poll>> GetAllAsync() => 
            await _context.Polls.AsNoTracking().ToListAsync();

        public async Task<Poll?> GetByIdAsync(int id) =>
            await _context.Polls.FindAsync(id);

        public async Task<Poll> AddPollAsync(Poll model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }



        //public Poll? GetById(int id) => _polls.SingleOrDefault(pol => pol.Id == id);

        //public Poll Add(Poll request)
        //{
        //    request.Id = _polls.Count + 1;
        //    _polls.Add(request);
        //    return request;
        //}

        //public bool Update(int id, Poll poll)
        //{
        //    var curPoll = GetById(id);
        //    if (curPoll == null)
        //    {
        //        return false;
        //    }
        //    curPoll.Title = poll.Title;
        //    curPoll.Summary = poll.Summary;
        //    return true;
        //}

        //public bool Delete(int id)
        //{
        //    var poll = GetById(id);
        //    if(poll == null)
        //    {
        //        return false;
        //    }
        //    _polls.Remove(poll);
        //    return true;
        //}
    }
}
