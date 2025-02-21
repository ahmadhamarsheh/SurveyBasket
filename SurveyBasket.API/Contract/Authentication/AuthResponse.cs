namespace SurveyBasket.API.Contract.Authentication
{
    public record AuthResponse(
        string Id,
        string? email,
        string FirstName,
        string LastName,
        string Token,
        int ExpiresIn
        );
}
