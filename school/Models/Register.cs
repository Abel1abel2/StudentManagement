using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace school.Models;
public class Register
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int StudentId { get; set; }
    [ForeignKey("StudentId")]
    public virtual Student? Student { get; set; }
    [Required]
    public int CourseId { get; set; }
    [ForeignKey("CourseId")]
    public virtual Course? Course { get; set; }
    [RegularExpression("^[A-F][+-]?$")]
    public string Grade { get; set; }

}
