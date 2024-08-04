using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SessionsService.Data;
using SessionsService.Models;

namespace SessionsService.Controllers;

[ApiController]
[Route("[controller]")]
public class BreaksController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Break>> GetBreaks()
    {
        return await context.Breaks.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Break>> CreateBreak([FromBody] Break @break)
    {
        try
        {
            context.Breaks.Add(@break);
            await context.SaveChangesAsync();
            return @break;
        }
        catch (Exception)
        {
            return new BadRequestResult();
        }
    }
}