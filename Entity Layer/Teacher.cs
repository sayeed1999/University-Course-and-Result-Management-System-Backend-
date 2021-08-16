using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long Contact { get; set; }
        public Designation? Designation { get; set; }
        public int DesignationId { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public float CreditToBeTaken { get; set; }
    }
}
