using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Grade
    {
        public long Id { get; set; }
        public string Name {  get; set; }
        public ICollection<StudentCourse> StudentsCourses { get; set; }
        public ICollection<StudentCourseHistory> StudentCourseHistories { get; set; }
    }
}
