using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Grade
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(4)]
    public string CourseCode { get; set; }
    [Required]
    public int grade { get; set; }
    public Student Student { get; set; }

    public Grade(string courseCode, int grade, Student student)
    {
        CourseCode = courseCode;
        this.grade = grade;
        Student = student;
    }

    private Grade(){}
}