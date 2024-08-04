using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsService.Data;
using StudentsService.Models;

namespace StudentsService.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet("{studentId:int}")]
    public async Task<ActionResult<Student>> GetStudentFromId([FromRoute] int studentId)
    {
        var student = await context.Students.Where(student => student.StudentId == studentId).FirstOrDefaultAsync();

        if (student == null)
        {
            return new NotFoundResult();
        }

        return Ok(student);
    }

    [HttpPut]
    public async Task<ActionResult<Student>> UpdateStudent([FromBody] Student student)
    {
        try
        {
            context.Update(student);
            await context.SaveChangesAsync();
            return Ok(student);
        }
        catch (Exception)
        {
            if (!IsStudentExists(student.StudentId))
            {
                return new NotFoundResult();
            }

            return new BadRequestResult();
        }
    }

    private bool IsStudentExists(int id)
    {
        return context.Students.Any(student => student.StudentId == id);
    }
}