using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.API.Contract.Questions;
using SurveyBasket.API.Errors;

namespace SurveyBasket.API.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController(IQuestionServices questionServices) : ControllerBase
    {
        private readonly IQuestionServices _questionServices = questionServices;

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromRoute] int pollId, CancellationToken cancellationToken = default)
        {
            var result = await _questionServices.GetAllAsync(pollId, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll([FromRoute] int pollId, [FromRoute] int Id, CancellationToken cancellationToken = default)
        {
            var result = await _questionServices.GetAsync(pollId, Id, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromRoute] int pollId, [FromBody] QuestionRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _questionServices.AddAsync(pollId, request, cancellationToken);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetAll), new { pollId, result.Value.Id }, result.Value);

            return result.Error.Equals(QuestionError.DuplicatedQuestionContent)
                ? result.ToProblem(StatusCodes.Status409Conflict)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }
    }
}
