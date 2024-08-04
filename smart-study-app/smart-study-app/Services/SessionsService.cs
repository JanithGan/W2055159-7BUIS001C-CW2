using SmartStudyApp.Models;

namespace SmartStudyApp.Services;

public class SessionsService(HttpClient httpClient)
{
    private const string SessionsServiceUri = "http://localhost:5102";

    public async Task<List<StudySession>?> GetStudySessions()
    {
        return await httpClient.GetFromJsonAsync<List<StudySession>>($"{SessionsServiceUri}/StudySessions");
    }

    public async Task<HttpResponseMessage> CreateStudySession(StudySession studySession)
    {
        return await httpClient.PostAsJsonAsync($"{SessionsServiceUri}/StudySessions", studySession);
    }

    public async Task<List<Break>?> GetBreaks()
    {
        return await httpClient.GetFromJsonAsync<List<Break>>($"{SessionsServiceUri}/Breaks");
    }

    public async Task<HttpResponseMessage> CreateBreak(Break @break)
    {
        return await httpClient.PostAsJsonAsync($"{SessionsServiceUri}/Breaks", @break);
    }
}