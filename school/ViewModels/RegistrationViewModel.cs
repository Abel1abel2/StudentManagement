using Microsoft.AspNetCore.Mvc.Rendering;
using school.DTOs;
using System.ComponentModel.DataAnnotations;

namespace school.ViewModels
{
    public class RegistrationViewModel
    {

        public SaveRegistrationDto RegistrationData { get; set; } = new(); // Replaces direct individual fields
        public List<SelectListItem> StudentOptions { get; set; } = new();
        public List<SelectListItem> CourseOptions { get; set; } = new();

    }
}
