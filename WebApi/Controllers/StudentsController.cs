using Microsoft.AspNetCore.Mvc;
using Shared;
using WebApi.Data;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IDataAccess dataAccess;

    public StudentsController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }
    
    [HttpPost]
    public async Task<ActionResult<Student>> CreateAsync(Student player)
    {
        try
        {
            await dataAccess.CreateStudentAsync(player);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAllAsync()
    {
        try
        {
            var result = await dataAccess.GetAllStudentsAsync();
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost, Route("Grade")]
    public async Task<ActionResult<Grade>> AddGradeToStudentAsync(Grade grade)
    {
        try
        {
            await dataAccess.AddGradeToStudentAsync(grade);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}