using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class GradeLetter
    {
        public string Grade {  get; set; }
        public ICollection<StudentCourse> StudentsCourses { get; set; } = new HashSet<StudentCourse>();
        public ICollection<StudentCourseHistory> StudentCourseHistories { get; set; } = new HashSet<StudentCourseHistory>();
    }
}
