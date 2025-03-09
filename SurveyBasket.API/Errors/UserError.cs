using SurveyBasket.API.Abstractions;
namespace SurveyBasket.API.Errors
{
    public static class UserError
    {
        public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "Invalid email/password");
    }
}

