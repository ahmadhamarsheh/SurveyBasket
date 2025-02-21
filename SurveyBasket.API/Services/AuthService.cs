
using Microsoft.AspNetCore.Identity;

namespace SurveyBasket.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            
            //return new response
            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName ,"Token", 3600);
        }
    }
}
