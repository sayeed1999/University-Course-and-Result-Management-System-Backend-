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
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesWithAllocatedRoomsByDepartment(long departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = from course in _dbSet
                                       where course.DepartmentId == departmentId
                                       select new Course
                                       {
                                           Code = course.Code,
                                           Name = course.Name,
                                           AllocateClassrooms = (ICollection<AllocateClassroom>)
                                                                _dbContext.AllocateClassrooms
                                                                          .Where(x => x.CourseId == course.Id)
                                                                          .Include(x => x.Room)
                                                                          .Include(x => x.Day)
                                       };
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
