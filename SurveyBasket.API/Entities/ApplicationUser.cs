using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.API.Entities
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<RefreshTokens> RefreshTokens { get; set; } = [];
    }
}
