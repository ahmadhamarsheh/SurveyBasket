
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController(IPollServices pollServices) : ControllerBase
    {
        private readonly IPollServices _pollServices = pollServices;

        [HttpGet("GetAll")]   
        public async Task<IActionResult> GetAll() => Ok(await _pollServices.GetAllAsync());

        [HttpGet]
        [Route("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var item = _pollServices.GetByIdAsync(Id);

            return Ok(item.Adapt<PollResponse>());
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] PollRequest request)
        {
            
            var newPoll = await _pollServices.AddPollAsync(request.MapToPoll());
            return CreatedAtAction(nameof(GetById), new { Id = newPoll.Id }, newPoll);
        }

        //[HttpPut]
        //[Route("Update/{id}")]
        //public IActionResult Update(int id, Poll request)
        //{
        //    var isUpdated = _pollServices.Update(id, request);
        //    return  isUpdated == true ? NoContent() : NotFound();
        //}

        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var isDelete = _pollServices.Delete(id);
        //    return isDelete == true ? NoContent() : NotFound();
        //}
    }
}
 