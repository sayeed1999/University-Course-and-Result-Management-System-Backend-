using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class CourseAssignToTeacher
    {
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public string CourseCode {  get; set; }
    }
}
