using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class CourseHistory
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public Semister Semister { get; set; }
        public long SemisterId { get; set; }
        public Teacher? Teacher { get; set; }
        public long? TeacherId { get; set; }
        public long NthHistory { get; set; }
    }
}
