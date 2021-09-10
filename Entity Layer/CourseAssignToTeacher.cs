using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class CourseAssignToTeacher
    {
        public long DepartmentId { get; set; }
        public long TeacherId { get; set; }
        public long CourseId {  get; set; }
    }
}
