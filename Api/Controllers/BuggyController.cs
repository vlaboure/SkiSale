using Api.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    // pour le test
    public class BuggyController : BaseApiController
    {
        // on fait appel direc au contexte et pas à un repo
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }
        // provocation des codes erreur 
        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            var response = _context.Products.Find(400);
            if(response == null)
             return NotFound(new ApiResponse(404));
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var response = _context.Products.Find(400);
            // this return a server error server
            var returnList = response.ToString();
            return Ok();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));        
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();        
        }
    }
}