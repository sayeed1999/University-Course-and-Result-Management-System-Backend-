using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class CourseHistory
    {
        public long Id {  get; set; }
        public string Code { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public Semister? Semister { get; set; }
        public byte SemisterId { get; set; }
        public Teacher? Teacher { get; set; }
        public int? TeacherId { get; set; }
        public UnassignCoursesCount UnassignCoursesCount { get; set; }
        public int UnassignCoursesCountId { get; set; }
    }
}
