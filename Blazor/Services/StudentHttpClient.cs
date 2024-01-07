using System.Text.Json;
using Shared;

namespace Blazor.Services;

public class StudentHttpClient : IStudentService
{
    private readonly HttpClient client;

    public StudentHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task CreateAsync(Student student)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Students", student);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/Students");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception(content);

        IEnumerable<Student> players = JsonSerializer.Deserialize<IEnumerable<Student>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return players;    
    }
    
    public async Task AddGradeToStudentAsync(Grade grade)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Students/Grade", grade);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}