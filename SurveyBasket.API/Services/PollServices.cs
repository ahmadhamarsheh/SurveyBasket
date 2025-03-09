using SurveyBasket.API.Abstractions;
using SurveyBasket.API.Errors;

namespace SurveyBasket.API.Services
{
    public class PollServices(ApplicationDbContext context) : IPollServices
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken) => 
            await _context.Polls.AsNoTracking().ToListAsync();

        public async Task<Result<PollResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var poll = await _context.Polls.FindAsync(id, cancellationToken);
            return poll is not null
                ? Result.Success(poll.Adapt<PollResponse>())
                : Result.Failure<PollResponse>(PollError.IdNotFound);
        }
            
        public async Task<PollResponse> AddPollAsync(PollRequest model, CancellationToken cancellationToken)
        {
            var pollModel = model.Adapt<Poll>();
            await _context.AddAsync(model, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return pollModel.Adapt<PollResponse>();
        }

        public async Task<Result> UpdateAsync(int id, PollRequest poll, CancellationToken cancellationToken)
        {
            var curr = await _context.Polls.FindAsync(id, cancellationToken);
            if (curr != null)
            {
                curr.Summary = poll.Summary;
                curr.StartAt = poll.StartAt;
                curr.EndAt = poll.EndAt;
                curr.Title = poll.Title;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            return Result.Failure(PollError.IdNotFound);
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var oldPoll = await GetByIdAsync(id, cancellationToken);
            if (oldPoll != null)
            {
                _context.Remove(oldPoll);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            return Result.Failure(PollError.IdNotFound);
        }
    }
}
