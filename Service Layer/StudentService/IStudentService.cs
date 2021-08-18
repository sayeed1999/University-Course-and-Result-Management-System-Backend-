using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.StudentService
{
    public interface IStudentService : IRepository<Student>
    {
        public Task<ServiceResponse<IEnumerable<Student>>> GetStudentsResults();
        public Task<ServiceResponse<Student>> RegisterStudent(StudentRegistration student);
        public Task<ServiceResponse<StudentCourse>> EnrollStudentInCourse(StudentCourse data);
        public Task<ServiceResponse<StudentCourse>> SaveResult(StudentCourse data);
    }
}
