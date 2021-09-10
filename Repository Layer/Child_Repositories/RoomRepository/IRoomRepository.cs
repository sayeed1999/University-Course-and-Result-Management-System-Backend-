using Entity_Layer;
using Repository_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        public Task<ServiceResponse<IEnumerable<AllocateClassroom>>> GetAllocatedRoomsByDepartment(int departmentId);
        public Task<ServiceResponse<AllocateClassroom>> AllocateClassroom(AllocateClassroom data);
        //public Task<ServiceResponse<List<AllocateClassroomHistory>>> UnallocateAllClassrooms();
    }
}
