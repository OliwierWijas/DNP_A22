using System.Text.Json;
using Shared;

namespace Blazor.Services;

public class GradeHttpClient : IGradeService
{
    private readonly HttpClient client;

    public GradeHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<StatisticsOverviewDto> GetCourseStatisticsAsync(string courseCode)
    {
        HttpResponseMessage response = await client.GetAsync($"/Grades/{courseCode}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception(content);
        
        return JsonSerializer.Deserialize<StatisticsOverviewDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}