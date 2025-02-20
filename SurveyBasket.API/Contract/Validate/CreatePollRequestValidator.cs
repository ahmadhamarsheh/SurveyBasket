namespace SurveyBasket.API.Contract.Validate
{
    public class CreatePollRequestValidator : AbstractValidator<PollRequest>
    {
        public CreatePollRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please add title").Length(3,20);
            RuleFor(x => x.StartAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
            RuleFor(x => x.EndAt)
                .NotEmpty()
                .GreaterThan(x => x.StartAt);
        }
    }
}
