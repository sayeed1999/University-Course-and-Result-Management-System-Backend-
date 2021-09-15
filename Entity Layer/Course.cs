using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Course
    {
        public long Id { get; set; }
        public string Code { get; set; } // atleast 5chars, unique
        public string Name { get; set; } // unique
        public float Credit { get; set; } // 0.5 - 5.0
        public string Description { get; set; }
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public Semister Semister { get; set; }
        public long SemisterId { get; set; }
        public Teacher? Teacher { get; set; }
        public long? TeacherId {  get; set; }
        public ICollection<StudentCourse> StudentsCourses { get; set; }
        public ICollection<AllocateClassroom> AllocateClassrooms { get; set; }
        public ICollection<AllocateClassroomHistory> AllocateClassroomHistories { get; set; }
        public ICollection<CourseHistory> CourseHistories { get; set; }
        public ICollection<StudentCourseHistory> StudentCourseHistories { get; set; }
    }
}
