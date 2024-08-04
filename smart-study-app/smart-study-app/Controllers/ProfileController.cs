using Microsoft.AspNetCore.Mvc;
using SmartStudyApp.Models;
using SmartStudyApp.Services;

namespace SmartStudyApp.Controllers;

public class ProfileController(StudentsService studentsService) : Controller
{
    // Hardcoded for one student
    private const int DefaultStudentId = 1;

    public async Task<IActionResult> Index()
    {
        return await ViewStudent();
    }

    public async Task<IActionResult> Edit()
    {
        return await ViewStudent();
    }

    private async Task<IActionResult> ViewStudent()
    {
        var student = await studentsService.GetStudentById();

        if (student == null) return NotFound();

        await SetModulesForStudent(student);

        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("StudentId,Name,StudentNumber,EnrolledModuleCodes")]
        Student student)
    {
        if (!ModelState.IsValid) return View(student);

        var response = await studentsService.UpdateStudent(student);
        response.EnsureSuccessStatusCode();

        return RedirectToAction(nameof(Index));
    }

    private async Task SetModulesForStudent(Student student)
    {
        var studentModuleCodes = student.EnrolledModuleCodes;

        // Get all modules
        var modules = await studentsService.GetModules() ?? [];

        ViewBag.Modules = modules;
        ViewBag.EnrolledModules = modules.Where(m => studentModuleCodes.Contains(m.ModuleCode)).ToList();
        ViewBag.UnEnrolledModules = modules.Where(m => !studentModuleCodes.Contains(m.ModuleCode)).ToList();
    }
}