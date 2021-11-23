using Microsoft.AspNetCore.Mvc;

namespace RestApiDemo.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}
