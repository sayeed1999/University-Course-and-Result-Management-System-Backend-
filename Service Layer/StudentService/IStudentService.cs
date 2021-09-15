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
        public Task<ServiceResponse<IEnumerable<Student>>> GetAllIncludingAll(string regNum); // Story 10
        public Task<ServiceResponse<Student>> RegisterStudent(StudentRegistration student); // Story 07
        public Task<ServiceResponse<Student>> GetStudentByEmail(string email);
        public Task<ServiceResponse<Student>> GetStudentByRegNum(string regNum);
        public Task<ServiceResponse<StudentCourse>> EnrollStudentInCourse(StudentCourse studentCourse); // Story 10
        public Task<ServiceResponse<StudentCourse>> SaveResult(StudentCourse data); // Story 11
        public Task<ServiceResponse<Student>> GetStudentResultById(long id); // Story 12

    }
}
