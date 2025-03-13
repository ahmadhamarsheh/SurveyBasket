namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PollsController(IPollServices pollServices) : ControllerBase
    {
        private readonly IPollServices _pollServices = pollServices;

        [HttpGet("GetAll")]   
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default) => Ok(await _pollServices.GetAllAsync(cancellationToken));

        [HttpGet]
        [Route("GetById/{Id}")]
        public async Task<IActionResult> GetById([FromRoute]int Id, CancellationToken cancellationToken = default)
        {
            var item = await _pollServices.GetByIdAsync(Id, cancellationToken);

            return item.IsT0 ?  Ok(item.AsT0) 
                : Problem(statusCode: StatusCodes.Status404NotFound, title: item.AsT1.Code, detail: item.AsT1.Description);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellationToken = default)
        {
            
            var newPoll = await _pollServices.AddPollAsync(request, cancellationToken);
            return newPoll.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { Id = newPoll.Value.Id }, newPoll.Value.Adapt<PollResponse>())
                : newPoll.ToProblem(StatusCodes.Status409Conflict, "Internal Server Error");
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id, PollRequest request, CancellationToken cancellationToken = default)
        {
            var isUpdated = await _pollServices.UpdateAsync(id, request, cancellationToken);
            return isUpdated.IsSuccess ? NoContent() 
                : Problem(statusCode: StatusCodes.Status404NotFound, title: isUpdated.Error.Code, detail: isUpdated.Error.Description);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var isDelete = await _pollServices.DeleteAsync(id,cancellationToken);
            return isDelete.IsSuccess ? NoContent() 
                : Problem(statusCode: StatusCodes.Status404NotFound, title: isDelete.Error.Code, detail: isDelete.Error.Description);
        }
    }
}
 