using System.Collections;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using SmartStudyApp.Models;
using SmartStudyApp.Services;

namespace SmartStudyApp.Controllers;

public class SessionsController(SessionsService sessionsService) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.Breaks = await sessionsService.GetBreaks() ?? [];
        ViewBag.StudySessions = await sessionsService.GetStudySessions() ?? [];
        return View();
    }

    public async Task<IActionResult> ExportReport()
    {
        var studySessions = await sessionsService.GetStudySessions() ?? [];

        var studySessionsRecords = studySessions.Select(s => new ReportRecord
        {
            Type = "StudySession",
            Module = s.ModuleCode,
            StartTime = s.StartTime.ToString(),
            EndTime = s.EndTime.ToString(),
            IsRecurrent = s.IsRecurrent,
            Notes = s.Notes
        }).ToList();

        var breaks = await sessionsService.GetBreaks() ?? [];

        var breaksRecords = breaks.Select(s => new ReportRecord
        {
            Type = "Break",
            Module = "None",
            StartTime = s.StartTime.ToString(),
            EndTime = s.EndTime.ToString(),
            IsRecurrent = s.IsRecurrent,
            Notes = s.Notes
        }).ToList();

        var combinedData = studySessionsRecords.Concat(breaksRecords).ToList();
        combinedData.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));

        // Assign row numbers
        for (var i = 0; i < combinedData.Count; i++) combinedData[i].Number = i + 1;

        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream);
        using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture));

        csvWriter.WriteRecords((IEnumerable)combinedData);
        streamWriter.Flush();
        return File(memoryStream.ToArray(), "text/csv", DateTime.Now + "-smart-study-report.csv");
    }
}