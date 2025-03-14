using SurveyBasket.API.Contract.Answers;

namespace SurveyBasket.API.Contract.Questions
{
    public record QuestionResponse(
        int Id,
        string Content,
        IEnumerable<AnswerResponse> Answers
    );
}
