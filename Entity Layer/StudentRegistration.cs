using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class StudentRegistration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long Contact { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
