using Data_Access_Layer;
using Entity_Layer;
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
    }
}
