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

        /*
public async Task<ServiceResponse<StudentCourse>> SaveResult(StudentCourse data)
{
   var serviceResponse = new ServiceResponse<StudentCourse>();
   try
   {
       var course = await _dbContext.StudentsCourses.FindAsync(data.DepartmentId, data.CourseCode, data.StudentId);
       course.Grade = data.Grade;
       _dbContext.StudentsCourses.Update(course);
       await _dbContext.SaveChangesAsync();
       serviceResponse.Data = course;
   }
   catch (Exception ex)
   {
       serviceResponse.Message = $"Something failed. Please try with proper data.\nError: {ex.Message}";
       serviceResponse.Success = false;
   }
   return serviceResponse;
}

public async Task<ServiceResponse<IEnumerable<Student>>> GetStudentsResults()
{
   var serviceResponse = new ServiceResponse<IEnumerable<Student>>();
   try
   {
       serviceResponse.Data = await _dbContext.Students
                           .Include(x => x.Department)
                           .Include(x => x.StudentsCourses)
                               .ThenInclude(z => z.Course)
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

public async Task<ServiceResponse<Student>> GetStudentResultById(long id)
{
   var serviceResponse = new ServiceResponse<Student>();
   try
   {
       serviceResponse.Data = await _dbContext.Students
                           .Include(x => x.Department)
                           .Include(x => x.StudentsCourses)
                               .ThenInclude(z => z.Course)
                           .SingleOrDefaultAsync(x => x.Id == id);

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

public async Task<ServiceResponse<Student>> GetStudentResultByRegNo(string reg)
{
   var serviceResponse = new ServiceResponse<Student>();
   try
   {
       serviceResponse.Data = await _dbContext.Students
                           .Include(x => x.Department)
                           .Include(x => x.StudentsCourses)
                               .ThenInclude(z => z.Course)
                           .SingleOrDefaultAsync(x => x.RegistrationNumber == reg);

       serviceResponse.Message = "Data fetched successfully from the database";
   }
   catch (Exception ex)
   {
       serviceResponse.Message = ex.Message;
       serviceResponse.Success = false;
   }
   if(serviceResponse.Data == null)
   {
       serviceResponse.Success = false;
       serviceResponse.Message = "Data not found";
   }
   return serviceResponse;
}
*/
    }
}
