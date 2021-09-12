using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<long> CountStudentsInDepartment(long departmentId)
        {
            long count = await _dbSet.CountAsync(x => x.DepartmentId == departmentId);
            return count;
        }

        public async Task<ServiceResponse<Student>> GetStudentByEmail(string email)
        {
            var serviceResponse = new ServiceResponse<Student>();
            serviceResponse.Data = await _dbSet.SingleOrDefaultAsync(x => x.Email == email);
            return serviceResponse;
        }

        public async Task<ServiceResponse<Student>> GetStudentByRegNum(string regNum)
        {
            var serviceResponse = new ServiceResponse<Student>();
            serviceResponse.Data = await _dbSet.SingleOrDefaultAsync(x => x.RegistrationNumber == regNum);
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<IEnumerable<Student>>> GetAll(string regNum = "")
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Student>>();
            try
            {
                serviceResponse.Data = await _dbContext.Students
                    .Include(x => x.Department)
                    .Include(x => x.StudentsCourses)
                        .ThenInclude(y => y.Course)
                    .Where(x => x.RegistrationNumber.Contains(regNum))
                    .Take(10)
                    .ToListAsync();

                serviceResponse.Message = "Data fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Some error occurred while fetching data.\nError message: " + ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<StudentCourse>> EnrollStudentInCourse(StudentCourse data)
        {
            var serviceResponse = new ServiceResponse<StudentCourse>();
            serviceResponse.Data = data;
            try
            {
                await _dbContext.StudentsCourses.AddAsync(data);
                serviceResponse.Message = "Successfully enrolled.";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<bool> IsStudentEnrolledInCourse(long studentId, long courseId)
        {
            StudentCourse sc = await _dbContext.StudentsCourses.SingleOrDefaultAsync(x => x.StudentId == studentId && x.CourseId == courseId);
            return sc != null;
        }

        public async Task<ServiceResponse<StudentCourse>> SaveResult(StudentCourse data)
        {
            var serviceResponse = new ServiceResponse<StudentCourse>();
            try
            {
                StudentCourse studentCourse = await _dbContext.StudentsCourses.FindAsync(data.Id);
                studentCourse.GradeId = data.GradeId;
                _dbContext.StudentsCourses.Update(studentCourse);
                serviceResponse.Data = studentCourse;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Student>> GetStudentResultById(long id)
        {
            var serviceResponse = new ServiceResponse<Student>();
            try
            {
                serviceResponse.Data = await _dbSet.Include(x => x.Department)
                                                   .Include(x => x.StudentsCourses)
                                                   .SingleOrDefaultAsync(x => x.Id == id);

                if(serviceResponse.Data != null)
                {
                    serviceResponse.Data.StudentsCourses = await _dbContext.StudentsCourses.Where(x => x.StudentId == id)
                                                                                     .Include(x => x.Course)
                                                                                     .Include(x => x.Grade)
                                                                                     .ToListAsync();
                }

                serviceResponse.Message = "Data fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            if (serviceResponse.Data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Data not found";
            }
            return serviceResponse;
        }

    }
}
