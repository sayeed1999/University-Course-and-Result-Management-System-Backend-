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

        public override async Task<ServiceResponse<Course>> Add(Course item)
        {
            var serviceResponse = new ServiceResponse<Course>();

            // find if there remains a course with the same name in the same department
            if (await _dbContext.Courses.SingleOrDefaultAsync(x => (x.Code == item.Code || x.Name == item.Name) && x.DepartmentId == item.DepartmentId) == null)
            {
                serviceResponse.Data = item;
                serviceResponse.Message = "Code and name must be unique in the respective department";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            try
            {
                _dbContext.Courses.Add(item);
                await _dbContext.SaveChangesAsync();
                serviceResponse.Data = item;
                serviceResponse.Message = "Item stored successfully to the database.";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

    }
}
