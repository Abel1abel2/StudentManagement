using System.ComponentModel.DataAnnotations;

namespace school.DTOs
{
    // Used for transferring individual registration details safely
    public class RegistrationDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Grade input is required.")]
        [RegularExpression("^[A-F][+-]?$")]
        public string Grade { get; set; } = string.Empty;
    }

    // Used specifically for safe create and update input commands
    public class SaveRegistrationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a student.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please select a course.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Grade input is required.")]
        [RegularExpression("^[A-F][+-]?$")]
        public string Grade { get; set; } = string.Empty;
    }
}
