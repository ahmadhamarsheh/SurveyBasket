using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SurveyBasket.API.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        public (string token, int expiresIn) GenerateToken(ApplicationUser user)
        {
            Claim[] claims = [
                new (JwtRegisteredClaimNames.Sub, user.Id),
                new (JwtRegisteredClaimNames.GivenName, user.FirstName),
                new (JwtRegisteredClaimNames.FamilyName, user.LastName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                ];

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("68WkIxbdeF4EyZW6kQ6d1FDThBROMJsF"));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var expiresIn = 30;

            var expirationDate = DateTime.UtcNow.AddMinutes(expiresIn);

            var token = new JwtSecurityToken(
                issuer : "SurveyBasketApp",
                audience : "SurveyBasketApp users",
                claims : claims,
                signingCredentials : signingCredentials,
                expires : expirationDate
                );

            return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn * 60);
        }
    }
}
