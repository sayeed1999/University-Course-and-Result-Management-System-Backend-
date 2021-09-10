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
        //public Task<ServiceResponse<IEnumerable<Student>>> GetAll(string regNum);
        //public Task<ServiceResponse<IEnumerable<Student>>> GetStudentsResults();
        //public Task<ServiceResponse<Student>> GetStudentResultById(long id);
        //public Task<ServiceResponse<Student>> GetStudentResultByRegNo(String reg);
        //public Task<ServiceResponse<Student>> RegisterStudent(StudentRegistration student);
        //public Task<ServiceResponse<StudentCourse>> EnrollStudentInCourse(StudentCourse data);
        //public Task<ServiceResponse<StudentCourse>> SaveResult(StudentCourse data);
    }
}
