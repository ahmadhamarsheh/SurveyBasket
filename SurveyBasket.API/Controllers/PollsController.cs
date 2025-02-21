
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurveyBasket.API.Contract.Polls;

namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController(IPollServices pollServices) : ControllerBase
    {
        private readonly IPollServices _pollServices = pollServices;

        [HttpGet("GetAll")]   
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default) => Ok(await _pollServices.GetAllAsync(cancellationToken));

        [HttpGet]
        [Route("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id, CancellationToken cancellationToken = default)
        {
            var item = await _pollServices.GetByIdAsync(Id, cancellationToken);

            return Ok(item.Adapt<PollResponse>());
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellationToken = default)
        {
            
            var newPoll = await _pollServices.AddPollAsync(request.MapToPoll(), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { Id = newPoll.Id }, newPoll);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id, Poll request, CancellationToken cancellationToken = default)
        {
            var isUpdated = await _pollServices.UpdateAsync(id, request, cancellationToken);
            return isUpdated == true ? NoContent() : NotFound();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var isDelete = await _pollServices.DeleteAsync(id,cancellationToken);
            return isDelete == true ? NoContent() : NotFound();
        }
    }
}
 