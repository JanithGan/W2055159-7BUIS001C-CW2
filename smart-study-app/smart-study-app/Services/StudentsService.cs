using SmartStudyApp.Models;

namespace SmartStudyApp.Services;

public class StudentsService(HttpClient httpClient)
{
    private const string StudentsServiceUri = "http://localhost:5101";
    private const int DefaultStudentId = 1;

    public async Task<Student?> GetStudentById()
    {
        return await httpClient.GetFromJsonAsync<Student>($"{StudentsServiceUri}/Students/{DefaultStudentId}");
    }

    public async Task<HttpResponseMessage> UpdateStudent(Student student)
    {
        return await httpClient.PutAsJsonAsync($"{StudentsServiceUri}/Students", student);
    }

    public async Task<List<Module>?> GetModules()
    {
        return await httpClient.GetFromJsonAsync<List<Module>>($"{StudentsServiceUri}/Modules");
    }
}