namespace SurveyBasket.API.Contract.Response
{
    public record PollResponse(int Id, string Title, string Summary, bool IsPublished, DateOnly StartAt, DateOnly EndAt);
}
