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

            return item.IsSuccess ?  Ok(item.Value) 
                : Problem(statusCode: StatusCodes.Status404NotFound, title: item.Error.Code, detail: item.Error.Description);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellationToken = default)
        {
            
            var newPoll = await _pollServices.AddPollAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { Id = newPoll.Id }, newPoll.Adapt<PollResponse>());
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
            return isDelete.IsSuccess ? NoContent() : NotFound(isDelete.Error);
        }
    }
}
 