using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Errors;

namespace Talabat.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseControllerApi
    {
        public ActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }

    }
}
