using Microsoft.AspNetCore.Mvc;
using SmartStudyApp.Models;
using SmartStudyApp.Services;

namespace SmartStudyApp.Controllers;

public class StudySessionsController(StudentsService studentsService, SessionsService sessionsService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var studySessions = await sessionsService.GetStudySessions() ?? [];
        ViewBag.StudySessions = studySessions;
        return PartialView("_StudySessions", studySessions);
    }

    public async Task<IActionResult> Create()
    {
        var student = await studentsService.GetStudentById();

        if (student == null) return NotFound();

        // Get all modules
        var modules = await studentsService.GetModules() ?? [];
        ViewBag.Modules = modules;
        ViewBag.EnrolledModules = modules.Where(m => student.EnrolledModuleCodes.Contains(m.ModuleCode)).ToList();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("ModuleCode,StartTime,EndTime,Notes,IsRecurrent")]
        StudySession studySession)
    {
        if (!ModelState.IsValid) return View(studySession);

        await sessionsService.CreateStudySession(studySession);
        return RedirectToAction("Index", "Sessions");
    }
}