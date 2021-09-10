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
        public long Id { get; set; }
        public long DepartmentId { get; set; } // three
        public long CourseId { get; set; } // combines
        public Course Course { get; set; }
        public long StudentId { get; set; } // the PK
        public Student Student { get; set; }
        public DateTime Date {  get; set; }
        public long? GradeId {  get; set; }
        public Grade? Grade { get; set; }
    }
}
