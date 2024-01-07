using Microsoft.EntityFrameworkCore;
using Shared;

namespace WebApi.Data;

public class DataAccess : IDataAccess
{
    private readonly StudentContext context;

    public DataAccess(StudentContext context)
    {
        this.context = context;
    }

    public async Task CreateStudentAsync(Student student)
    {
        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await context.Students.ToListAsync();
    }

    public async Task AddGradeToStudentAsync(Grade grade)
    {
        context.Entry(grade.Student).State = EntityState.Unchanged;
        Grade? temp = await context.Grades.FirstOrDefaultAsync(g => g.Student.Id == grade.Student.Id && g.CourseCode.Equals(grade.CourseCode));
        if (temp is not null)
        {
            await context.Grades.Where(g => g.Student.Id == grade.Student.Id && g.CourseCode.Equals(grade.CourseCode))
                .ExecuteDeleteAsync();
        }
        await context.Grades.AddAsync(grade); 
        await context.SaveChangesAsync();
    }

    public async Task<StatisticsOverviewDto> GetCourseStatisticsAsync(string courseCode)
    {
        int int1 = await context.Grades.Where(g => g.CourseCode.Equals(courseCode)).CountAsync();
        int int2 = await context.Grades.Where(g => g.CourseCode.Equals(courseCode) && g.grade > 0).CountAsync();
        double double1 = await context.Grades.Where(g => g.CourseCode.Equals(courseCode)).AverageAsync(g => g.grade);
        double double2 = await context.Grades.Where(g => g.CourseCode.Equals(courseCode) && g.grade > 0).AverageAsync(g => g.grade);
        IList<Grade> grades = await context.Grades.Where(g => g.CourseCode.Equals(courseCode)).OrderBy(g => g.grade).ToListAsync();
        int median = grades[int1/2].grade;

        StatisticsOverviewDto dto = new StatisticsOverviewDto
        {
            CourseCode = courseCode,
            TotalNumberOfPassedStudents = int2,
            TotalNumberOfStudents = int1,
            AvgGradeOverall = (float)double1,
            AvgGradeOfPassed = (float)double2,
            MedianGrade = median
        };
        return dto;
    }
}