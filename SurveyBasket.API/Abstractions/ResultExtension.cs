namespace SurveyBasket.API.Abstractions
{
    public static class ResultExtension
    {
        public static ObjectResult ToProblem(this Result result, int status, string? title = null)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("cannot convert success to problem");
            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Extensions = new Dictionary<string, object?>
                {
                    { "error", result.Error},
                }
            };

            return new ObjectResult(problemDetails);
        }
    }
}
