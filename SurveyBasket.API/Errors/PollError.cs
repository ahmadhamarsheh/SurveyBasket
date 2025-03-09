using SurveyBasket.API.Abstractions;

namespace SurveyBasket.API.Errors
{
    public static class PollError
    {
        public static readonly Error IdNotFound = new("Poll.IdNotFound", "There is no poll with this Id");
        public static readonly Error PollNotValid = new("Poll.PollNotValid", "Poll was not valid, please ensure all input correct");
    }
}
