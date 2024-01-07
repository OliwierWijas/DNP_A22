using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Student
{
    [Key]
    public int Id { get; set; }
    [MaxLength(25), Required]
    public string Name { get; set; }
    [Required]
    public string Programme { get; set; }

    public Student(string name, string programme)
    {
        Name = name;
        Programme = programme;
    }

    private Student(){}
}