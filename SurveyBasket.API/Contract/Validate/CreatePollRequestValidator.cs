namespace SurveyBasket.API.Contract.Validate
{
    public class CreatePollRequestValidator : AbstractValidator<PollRequest>
    {
        public CreatePollRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please add title").Length(3,20);
        }
    }
}
