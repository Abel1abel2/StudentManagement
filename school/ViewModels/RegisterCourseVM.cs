using Microsoft.AspNetCore.Mvc.Rendering;
using school.Models;

using Microsoft.AspNetCore.Mvc.Rendering;
using school.Models;
using System.Collections.Generic;
namespace school.ViewModels
{
    public class RegisterCourseVM
    {
        public List<Register>? Registrations { get; set; }
        public SelectList? Courses { get; set; }

        public string? SelectedCourseName { get; set; }

        public string? SearchString { get; set; }
    }
}
