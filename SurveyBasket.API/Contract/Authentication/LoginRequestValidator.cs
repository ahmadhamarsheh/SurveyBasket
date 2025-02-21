using Microsoft.AspNetCore.Identity.Data;

namespace SurveyBasket.API.Contract.Authentication
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest> 
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().Length(3, 100).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Length(1, 50);
        }
    }
}
