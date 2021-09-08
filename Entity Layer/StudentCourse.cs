using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class StudentCourse
    {
        public int DepartmentId { get; set; } // three
        public string CourseCode { get; set; } // combines
        public long StudentId { get; set; } // the PK
        public DateTime Date {  get; set; }
        public string? Grade {  get; set; }
        public GradeLetter? GradeLetter { get; set; }
        public Student Student {  get; set; }
        public Course Course {  get; set; }
    }
}
