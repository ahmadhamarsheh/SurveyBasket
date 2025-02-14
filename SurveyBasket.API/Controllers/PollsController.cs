
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController(IPollServices pollServices) : ControllerBase
    {
        private readonly IPollServices _pollServices = pollServices;

        [HttpGet("GetAll")]   
        public IActionResult GetAll() => Ok(_pollServices.GetAll().MapAllToPollResponse());

        [HttpGet]
        [Route("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var item = _pollServices.GetById(Id);

            return Ok(item.Adapt<PollResponse>());    
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody]PollRequest request)
            //[FromServices] IValidator<PollRequest> validator)
        {
           /* var vaild = validator.Validate(request);
            if (!vaild.IsValid)
            {
                var modelError = new ModelStateDictionary();
                vaild.Errors.ForEach(x => modelError.AddModelError(x.PropertyName, x.ErrorMessage));

                return ValidationProblem
            }*/
            var newPoll = _pollServices.Add(request.MapToPoll());
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
