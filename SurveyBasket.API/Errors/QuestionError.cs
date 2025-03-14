using SurveyBasket.API.Abstractions;

namespace SurveyBasket.API.Errors
{
    public static class QuestionError
    {
        public static readonly Error QuestionNotFound = new("Question.IdNotFound", "There is no question with this Id");
        public static readonly Error DuplicatedQuestionContent = new("Question.DuplicatedQuestionContent", "Question was exist, please enter a different question title");
    }
}
