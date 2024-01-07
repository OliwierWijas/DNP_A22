using Microsoft.AspNetCore.Mvc;
using Shared;
using WebApi.Data;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GradesController : ControllerBase
{
    private readonly IDataAccess dataAccess;

    public GradesController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }
    
    [HttpGet, Route("{courseCode}")]
    public async Task<ActionResult<int>> GetTotalNumberOfStudentsInCourseAsync(string courseCode)
    {
        try
        {
            var result = await dataAccess.GetCourseStatisticsAsync(courseCode);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}