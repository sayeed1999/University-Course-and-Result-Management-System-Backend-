using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Department
    {
        public Department()
        {
            this.Courses = new HashSet<Course>();
            this.Teachers = new HashSet<Teacher>();
            this.Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}