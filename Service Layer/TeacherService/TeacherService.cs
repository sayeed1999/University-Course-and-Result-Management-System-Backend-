using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.TeacherService
{
    public class TeacherService : Repository<Teacher>, ITeacherService
    {
        public TeacherService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ServiceResponse<IEnumerable<Teacher>>> GetTeachersByDepartment(int departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Teacher>>();
            try
            {
                serviceResponse.Data = await _dbContext.Teachers
                    .Where(x => x.DepartmentId == departmentId)
                    .ToListAsync();
                serviceResponse.Message = "Data fetched successfully from the database";
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
