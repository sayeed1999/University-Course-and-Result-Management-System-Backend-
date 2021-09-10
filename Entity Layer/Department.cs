using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Department
    {
        public long Id { get; set; }
        public string Code { get; set; } // 2-7 chars, unique
        public string Name { get; set; } // unique
        public ICollection<Course> Courses { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<CourseHistory> CourseHistories { get; set; }
        public ICollection<AllocateClassroom> AllocateClassrooms { get; set; }
        public ICollection<AllocateClassroomHistory> AllocateClassroomHistories { get; set; }
    }
}