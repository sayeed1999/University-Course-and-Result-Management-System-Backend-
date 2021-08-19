using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.DepartmentService
{
    public class DepartmentService : Repository<Department>, IDepartmentService
    {
        public DepartmentService(ApplicationDbContext dbContext) : base(dbContext) { }

        public virtual async Task<ServiceResponse<IEnumerable<Department>>> GetAllIncludingTeachersAndCourses()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Department>>();
            try
            {
                serviceResponse.Data = await _dbContext.Departments
                        .Include(x => x.Teachers)
                        .Include(x => x.Courses).ThenInclude(x => x.Semister)
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

        public virtual async Task<ServiceResponse<IEnumerable<Department>>> GetAllIncludingCourses()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Department>>();
            try
            {
                serviceResponse.Data = await _dbContext.Departments
                        .Include(x => x.Courses).ThenInclude(x => x.AllocateClassrooms)
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

    }
}
