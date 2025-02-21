namespace SurveyBasket.API.Contract.Polls
{
    public record PollRequest(string Title, string Summary, bool IsPublished, DateOnly StartAt, DateOnly EndAt);
}
