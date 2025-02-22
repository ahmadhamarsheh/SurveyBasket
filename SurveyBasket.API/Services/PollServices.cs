namespace SurveyBasket.API.Services
{
    public class PollServices(ApplicationDbContext context) : IPollServices
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken) => 
            await _context.Polls.AsNoTracking().ToListAsync();

        public async Task<Poll?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var res = await _context.Polls.FindAsync(id, cancellationToken);
            return res;
        }
            
        public async Task<Poll> AddPollAsync(Poll model, CancellationToken cancellationToken)
        {
            await _context.AddAsync(model, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return model;
        }

        public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken)
        {
            var curr = await _context.Polls.FindAsync(id, cancellationToken);
            if (curr != null)
            {
                curr.Summary = poll.Summary;
                curr.StartAt = poll.StartAt;
                curr.EndAt = poll.EndAt;
                curr.IsPublished = poll.IsPublished;
                curr.Title = poll.Title;

                await _context.SaveChangesAsync(cancellationToken);
                return true;

            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var oldPoll = await GetByIdAsync(id, cancellationToken);
            if (oldPoll != null)
            {
                _context.Remove(oldPoll);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
    }
}
