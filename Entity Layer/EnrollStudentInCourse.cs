using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class EnrollStudentInCourse
    {
        public long StudentId {  get; set; }
        public int DepartmentId { get; set; }
        public string CourseCode {  get; set; }
        public DateTime Date { get; set; }
    }
}
