
using Microsoft.AspNetCore.Identity;
using SurveyBasket.API.Authentication;
using System.Security.Cryptography;

namespace SurveyBasket.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly int _refreshTokenExpiryDays = 14;
        public AuthService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider)
        {
            _context = context;
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }
        public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            //check if user exist?
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null) return null;

            //check pass
            var checkPass = await _userManager.CheckPasswordAsync(user, password);
            if(!checkPass) return null;

            //generate token
            var (token,expires) = _jwtProvider.GenerateToken(user);

            //generate refresh token
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            //save to DB
            user.RefreshTokens.Add(new RefreshTokens
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            //return new response
            return new AuthResponse(user.Id, user.Email,
                user.FirstName, user.LastName,
                token, expires, refreshToken,
                refreshTokenExpiration
            );
        }
        public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId == null) return null;
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return null;

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActived);

            if (userRefreshToken == null) return null;

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            //generate token
            var (newToken, expires) = _jwtProvider.GenerateToken(user);

            //generate refresh token
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            //save to DB
            user.RefreshTokens.Add(new RefreshTokens
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            //return new response
            return new AuthResponse(user.Id, user.Email,
                user.FirstName, user.LastName,
                newToken, expires, newRefreshToken,
                refreshTokenExpiration
            );
        }
        public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId == null) return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return false;

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActived);

            if (userRefreshToken == null) return false;

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return true;
        }
        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
