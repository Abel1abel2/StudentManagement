using System.ComponentModel.DataAnnotations;

namespace school.Models;

public class Course
{
    [Key]
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public int CreditHours { get; set; }


}
