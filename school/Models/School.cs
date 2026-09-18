using System.ComponentModel.DataAnnotations;

namespace school.Models
{//this is my student table
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department {  get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string? MobileNo { get; set; }
   


    }
}
