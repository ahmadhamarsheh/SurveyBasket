
using Microsoft.AspNetCore.Identity;
using SurveyBasket.API.Authentication;

namespace SurveyBasket.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
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
            //return new response
            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName ,token, expires);
        }
    }
}
