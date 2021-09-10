using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class StudentCourseHistory
    {
        public long Id { get; set; }
        public long DepartmentId { get; set; }
        public long CourseId { get; set; }
        public Course Course { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime Date { get; set; }
        public long? GradeId { get; set; }
        public Grade? Grade { get; set; }
        public long NthHistory {  get; set; }
    }
}
