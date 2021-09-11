using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.StudentService
{
    public interface IStudentService
    {
        public Task<ServiceResponse<Student>> RegisterStudent(StudentRegistration student); // Story 07
    }
}
