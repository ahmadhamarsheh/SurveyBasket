using SurveyBasket.API.Abstractions;

namespace SurveyBasket.API.Errors
{
    public static class PollError
    {
        public static readonly Error IdNotFound = new("Poll.IdNotFound", "There is no poll with this Id");
    }
}
