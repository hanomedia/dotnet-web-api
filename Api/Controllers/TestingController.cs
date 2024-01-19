using Api.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TestingController : BaseApiController
{
        private readonly ApplicationDbContext _context;
    public TestingController(ApplicationDbContext context)
    {
            _context = context;
    }
    [HttpGet("notfound")]
    public ActionResult GetNotFound()
    {
        var data = _context.Products.Find(54);
        if(data == null)
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok();
        
    }
    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var data = _context.Products.Find(54);
        var dataToReturn = data.ToString();
        return Ok();
        
    }
    [HttpGet("getbadrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
        
    }
    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequestValidation(int id)
    {
        return Ok();
        
    }
}
