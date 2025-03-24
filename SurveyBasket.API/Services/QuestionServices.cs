using SurveyBasket.API.Contract.Answers;
using SurveyBasket.API.Contract.Questions;
using SurveyBasket.API.Errors;

namespace SurveyBasket.API.Services
{
    public class QuestionServices(ApplicationDbContext context) : IQuestionServices
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken = default)
        {
            var pollIsExist = await _context.Polls.AnyAsync(x => x.Id == pollId, cancellationToken : cancellationToken);
            if (!pollIsExist)
                return Result.Failure<IEnumerable<QuestionResponse>>(PollError.IdNotFound);

            var allQuestions = await _context.Questions
                .Where(x => x.Id == pollId)
                .Include(x => x.Answers)
                .Select(q => new QuestionResponse(
                    q.Id,
                    q.Content,
                    q.Answers.Select(a => new AnswerResponse(a.Id, a.Content))
                ))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<QuestionResponse>>(allQuestions);
        }

        public async Task<Result<QuestionResponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellationToken = default)
        {
            var isExist = await _context.Polls.AnyAsync(x => x.Id == pollId, cancellationToken : cancellationToken);
            if (!isExist)
                return Result.Failure<QuestionResponse>(PollError.PollIsExist);

            var questionIsExists = await _context.Questions.AnyAsync(x => x.Content == request.Content && x.PollId == pollId, cancellationToken);
            if (questionIsExists)
                return Result.Failure<QuestionResponse>(QuestionError.DuplicatedQuestionContent);

            var question = request.Adapt<Question>();
            question.PollId = pollId;

            request.Answers.ForEach(answer => question.Answers.Add(new Answer { Content = answer }));

            await _context.AddAsync(question, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(question.Adapt<QuestionResponse>());
        }

        public async Task<Result<QuestionResponse>> GetAsync(int pollId, int Id, CancellationToken cancellationToken = default)
        {
            var question = await _context.Questions
                .Where(x => x.PollId == pollId && x.Id == Id)
                .Include(x => x.Answers)
                .ProjectToType<QuestionResponse>()
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if (question is null)
                return Result.Failure<QuestionResponse>(QuestionError.QuestionNotFound);
            return Result.Success(question);
        }

        public async Task<Result> ToggleStatusAsync(int pollId, int Id, CancellationToken cancellationToken = default)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(x => x.Id == Id && x.PollId == pollId);

            if (question is null)
                return Result.Failure<QuestionResponse>(QuestionError.QuestionNotFound);

            question.IsActive = !question.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(question);
        }
    }
}
