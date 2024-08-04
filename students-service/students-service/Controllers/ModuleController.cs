using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsService.Data;
using StudentsService.Models;

namespace StudentsService.Controllers;

[ApiController]
[Route("[controller]")]
public class ModulesController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Module>>> GetModules()
    {
        var modules = await context.Modules.ToListAsync();

        if (modules.Count == 0)
        {
            return new NotFoundResult();
        }

        return Ok(modules);
    }
}