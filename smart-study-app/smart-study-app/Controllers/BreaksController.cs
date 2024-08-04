using Microsoft.AspNetCore.Mvc;
using SmartStudyApp.Models;
using SmartStudyApp.Services;

namespace SmartStudyApp.Controllers;

public class BreaksController(SessionsService sessionsService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var breaks = await sessionsService.GetBreaks() ?? [];
        ViewBag.Breaks = breaks;
        return PartialView("_Breaks", breaks);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("StartTime,EndTime,Notes,IsRecurrent")] Break @break)
    {
        if (!ModelState.IsValid) return View(@break);

        await sessionsService.CreateBreak(@break);
        return RedirectToAction("Index", "Sessions");
    }
}