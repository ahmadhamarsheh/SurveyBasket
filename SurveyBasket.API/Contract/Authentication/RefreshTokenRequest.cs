﻿namespace SurveyBasket.API.Contract.Authentication
{
    public record RefreshTokenRequest(
        string Token,
        string RefreshToken
        );

}
