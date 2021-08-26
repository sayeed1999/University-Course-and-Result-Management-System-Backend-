using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.RoomService
{
    public interface IRoomService : IRepository<Room>
    {
        public Task<ServiceResponse<IEnumerable<AllocateClassroom>>> GetAllocatedRoomsByDepartment(int departmentId);
        public Task<ServiceResponse<AllocateClassroom>> AllocateClassroom(AllocateClassroom data);
        public Task<ServiceResponse<List<AllocateClassroomHistory>>> UnallocateAllClassrooms();
    }
}
