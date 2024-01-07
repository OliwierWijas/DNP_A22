using Shared;

namespace WebApi.Data;

public interface IDataAccess
{
    Task CreateStudentAsync(Student student);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task AddGradeToStudentAsync(Grade grade);
    
    Task<StatisticsOverviewDto> GetCourseStatisticsAsync(string courseCode);
}