using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.CourseService
{
    public class CourseService : Repository<Course>, ICourseService
    {
        public CourseService(ApplicationDbContext dbContext) : base(dbContext) { }

        public override async Task<ServiceResponse<IEnumerable<Course>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbContext.Courses
                    .Include(x => x.Teacher)
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

        public virtual async Task<ServiceResponse<Course>> GetByCompositeKey(int departmentId, string courseCode)
        {
            var serviceResponse = new ServiceResponse<Course>();
            try
            {
                serviceResponse.Data = await _dbContext.Courses.Include(x => x.Teacher)
                    .SingleOrDefaultAsync(x => x.DepartmentId == departmentId 
                                            && x.Code == courseCode);
                
                if (serviceResponse.Data == null)
                {
                    serviceResponse.Message = "Data not found with the given constrain.";
                    serviceResponse.Success = false;
                }
                else serviceResponse.Message = "Data  with the given id was fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Some error occurred while fetching data.\nError message: " + ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(int departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbContext.Courses
                    .Include(x => x.Teacher)
                    .Where(x => x.DepartmentId == departmentId)
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

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentCode(String departmentCode)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                var department = await _dbContext.Departments.SingleOrDefaultAsync(x => x.Code == departmentCode);
                serviceResponse.Data = await _dbContext.Courses.Where(x => x.DepartmentId == department.Id).ToListAsync();

                serviceResponse.Message = "Data fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Some error occurred while fetching data.\nError message: " + ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesWithAllocatedRoomsByDepartment(int departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbContext.Courses.Where(x => x.DepartmentId == departmentId)
                                                            .Include(x => x.AllocateClassrooms)
                                                            .ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
    }
}
