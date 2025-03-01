namespace SurveyBasket.API.Contract.Polls
{
    public record PollRequest(string Title, string Summary, DateOnly StartAt, DateOnly EndAt);
}
