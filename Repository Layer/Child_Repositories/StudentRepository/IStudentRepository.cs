using Entity_Layer;
using Repository_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        public Task<long> CountStudentsInDepartment(long departmentId);
        public Task<ServiceResponse<Student>> GetStudentByEmail(string email);
        public Task<ServiceResponse<Student>> GetStudentByRegNum(string regNum);
        public Task<ServiceResponse<IEnumerable<Student>>> GetAll(string regNum);
        public Task<ServiceResponse<StudentCourse>> EnrollStudentInCourse(StudentCourse data);
        public Task<bool> IsStudentEnrolledInCourse(long studentId, long courseId);
        public Task<ServiceResponse<StudentCourse>> SaveResult(StudentCourse data);
        public Task<ServiceResponse<Student>> GetStudentResultById(long id);

    }
}
