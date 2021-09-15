using Data_Access_Layer;
using Entity_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public class AllocateClassroomHistoryRepository : Repository<AllocateClassroomHistory>, IAllocateClassroomHistoryRepository
    {
        public AllocateClassroomHistoryRepository(ApplicationDbContext dbContext) :  base(dbContext)
        {

        }
    }
}
