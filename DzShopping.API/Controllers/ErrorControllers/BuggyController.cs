using DzShopping.API.Errors;
using DzShopping.Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace DzShopping.API.Controllers.ErrorControllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DzDbContext _dzDbContext;

        public BuggyController(DzDbContext dzDbContext)
        {
            _dzDbContext = dzDbContext;
        }

        [HttpGet("notFound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _dzDbContext.Products.Find(45);
            if (thing == null) return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("serverError")]
        public ActionResult GetServerError()
        {
            var thing = _dzDbContext.Products.Find(45);
            var thingToReturn = thing.ToString();
            return Ok(thingToReturn);
        }

        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badRequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
