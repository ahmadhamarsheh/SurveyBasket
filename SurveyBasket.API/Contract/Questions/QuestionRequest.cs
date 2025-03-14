namespace SurveyBasket.API.Contract.Questions
{
    public record QuestionRequest(string Content, List<string>Answers);
}
