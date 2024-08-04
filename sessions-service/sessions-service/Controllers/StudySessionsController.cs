using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SessionsService.Data;
using SessionsService.Models;

namespace SessionsService.Controllers;

[ApiController]
[Route("[controller]")]
public class StudySessionsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<StudySession>> GetStudySessions()
    {
        return await context.StudySessions.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<StudySession>> CreateStudySession([FromBody] StudySession studySession)
    {
        try
        {
            context.StudySessions.Add(studySession);
            await context.SaveChangesAsync();
            return studySession;
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }
}