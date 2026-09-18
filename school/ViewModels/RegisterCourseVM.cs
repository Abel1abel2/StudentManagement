using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Rendering;
using school.DTOs;
using school.Models;
using school.Models;
using System.Collections.Generic;
namespace school.ViewModels
{
    public class RegisterCourseVM
    {
        public List<RegistrationDto>? Registrations { get; set; } // Changed from List<Register>
        public SelectList? Courses { get; set; }
        public string? SelectedCourseName { get; set; }
        public string? SearchString { get; set; }
    }
}
