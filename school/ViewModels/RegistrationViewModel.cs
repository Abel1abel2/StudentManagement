using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace school.ViewModels
{
    public class RegistrationViewModel
    {
  
        [Required(ErrorMessage = "Please select a student.")]
        [Display(Name = "Student Name")]

        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please select a course.")]
        [Display(Name = "Course Name")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Grade input is required.")]
        [RegularExpression("^[A-F][+-]?$")]
        public string Grade { get; set; }

        public List<SelectListItem> StudentOptions { get; set; } = new();
        public List<SelectListItem> CourseOptions { get; set; } = new();
    }
}
