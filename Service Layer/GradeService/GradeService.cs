using Data_Access_Layer;
using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.GradeService
{
    public class GradeService : Repository<Grade>, IGradeService
    {
        public GradeService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
