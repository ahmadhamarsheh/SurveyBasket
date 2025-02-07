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
    }
}
