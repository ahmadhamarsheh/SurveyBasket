﻿using OneOf;
using SurveyBasket.API.Abstractions;

namespace SurveyBasket.API.Services
{
    public interface IAuthService
    {
        //Task <Result<AuthResponse?>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task <OneOf<AuthResponse, Error>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
    }
}
