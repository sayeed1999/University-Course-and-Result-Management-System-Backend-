using Data_Access_Layer;
using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.DesignationService
{
    public class DesignationService : Repository<Designation>, IDesignationService
    {
        public DesignationService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
