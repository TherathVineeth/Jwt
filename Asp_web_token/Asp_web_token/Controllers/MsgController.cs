using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp_web_token.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MsgController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Helo this is second controller";
        }
    }
}
