using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SurveyBasket.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtOptions _jwtOptions;  
        public AuthController(IAuthService authService, IOptions<JwtOptions> jwtOptions)
        {
            _authService = authService;
            _jwtOptions = jwtOptions.Value;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequest request, CancellationToken cancellationToken = default)
        {
            var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
            //return authResult is null ? BadRequest("Invalid email and password") : Ok(authResult);
            //return authResult.IsSuccess ? Ok(authResult) : Ok(authResult.Error);

            return authResult.Match<IActionResult>(
                authResponse => Ok(authResponse),
                error =>  Problem
                (
                    statusCode : StatusCodes.Status400BadRequest,
                    title : error.Code,
                    detail : error.Description
                )
            );
        }
        [HttpPost]
        [Route("Refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody]RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
            return authResult is null ? BadRequest("Invalid token") : Ok(authResult);
        }
        [HttpPost]
        [Route("RevokeRefresh")]
        public async Task<IActionResult> RevokeRefreshAsync([FromBody]RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var isRevoked = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
            return isRevoked ? Ok() : BadRequest("Operation failed");
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            return Ok(_jwtOptions.Audience);
        }
    }
}
