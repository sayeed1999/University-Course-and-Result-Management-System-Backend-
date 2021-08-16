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
    }
}
