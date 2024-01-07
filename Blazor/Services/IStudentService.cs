using Shared;

namespace Blazor.Services;

public interface IStudentService
{
    Task CreateAsync(Student student);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task AddGradeToStudentAsync(Grade grade);
}