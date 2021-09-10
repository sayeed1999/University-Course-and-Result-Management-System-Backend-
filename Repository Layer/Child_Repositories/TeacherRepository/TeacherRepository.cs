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
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<ServiceResponse<Teacher>> GetTeacherByEmail(string email)
        {
            var serviceResponse = new ServiceResponse<Teacher>();
            serviceResponse.Data = await _dbSet.SingleOrDefaultAsync(x => x.Email == email);
            return serviceResponse;
        }

        public Task<ServiceResponse<IEnumerable<Teacher>>> GetTeachersByDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<IEnumerable<TeacherView>>> GetTeachersByDepartmentWithAssignedCourses(long departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<TeacherView>>();
            try
            {
                IEnumerable<Teacher> teachers = await _dbSet.Where(x => x.DepartmentId == departmentId)
                                                   .Include(x => x.Courses)
                                                   .ToListAsync();
                serviceResponse.Message = "Data fetched successfully from the database";

                var teacherViews = from teacher in teachers
                                   select new TeacherView
                                   {
                                       Address = teacher.Address,
                                       Contact = teacher.Contact,
                                       Courses = teacher.Courses,
                                       CreditToBeTaken = teacher.CreditToBeTaken,
                                       DepartmentId = teacher.DepartmentId,
                                       DesignationId = teacher.DesignationId,
                                       Id = teacher.Id,
                                       Email = teacher.Email,
                                       Name = teacher.Name,
                                       RemainingCredit = teacher.CreditToBeTaken - teacher.Courses.Sum(x => x.Credit)
                                   };
                serviceResponse.Data = teacherViews;
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
