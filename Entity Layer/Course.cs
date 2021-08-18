using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Course
    {
        // Code + DepartmentId combinely forms the Composite Key which is the PK for thist table. This table has no single row PK.
        public string Code { get; set; }
        public string Name { get; set; }
        public float Credit { get; set; }
        public string Description { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public Semister? Semister { get; set; }
        public byte SemisterId { get; set; }
        public Teacher? Teacher { get; set; }
        public int? TeacherId {  get; set; }
        public ICollection<StudentCourse> StudentsCourses { get; set; }
    }
}
