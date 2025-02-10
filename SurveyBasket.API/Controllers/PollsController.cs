using SurveyBasket.API.Services;

namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController(IPollServices pollServices) : ControllerBase
    {
        private readonly IPollServices _pollServices = pollServices;

        [HttpGet("GetAll")]   
        public IActionResult GetAll() => Ok(_pollServices.GetAll());

        [HttpGet]
        [Route("GetById/{Id}")]
        public IActionResult GetById(int Id) => Ok(_pollServices.GetById(Id));

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Poll request)
        {
            var newPoll = _pollServices.Add(request);
            return CreatedAtAction(nameof(GetById), new { Id = newPoll.Id }, newPoll);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult Update(int id, Poll request)
        {
            var isUpdated = _pollServices.Update(id, request);
            return  isUpdated == true ? NoContent() : NotFound();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var isDelete = _pollServices.Delete(id);
            return isDelete == true ? NoContent() : NotFound();
        }
    }
}
