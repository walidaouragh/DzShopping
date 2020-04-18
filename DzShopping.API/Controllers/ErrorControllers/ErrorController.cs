using DzShopping.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DzShopping.API.Controllers.ErrorControllers
{
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}