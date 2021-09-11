using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class TeacherView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; } // uniquw and correct format
        public long Contact { get; set; }
        public Designation Designation { get; set; }
        public long DesignationId { get; set; }
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public float CreditToBeTaken { get; set; } // non-negative
        public float RemainingCredit { get; set; } // (0 - CreditToBeTaken)
        public ICollection<Course> Courses { get; set; }
    }
}
