using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using Repository_Layer.Repository;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<VIEW_Department>> ViewAllDepartmentsAsync()
        {
            try
            {
                return await _dbContext.VIEW_Departments.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Data fetching error from the database");
            }
        }

    }
}
