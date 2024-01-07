using Shared;

namespace Blazor.Services;

public interface IGradeService
{
    Task<StatisticsOverviewDto> GetCourseStatisticsAsync(string courseCode);
}