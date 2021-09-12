using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.RoomService
{
    public interface IRoomService
    {
        public Task<ServiceResponse<IEnumerable<Room>>> GetRooms(); // Story 08
        public Task<ServiceResponse<AllocateClassroom>> AllocateClassroom(AllocateClassroom data); // Story 08
        public Task<ServiceResponse<IEnumerable<AllocateClassroom>>> UnallocateAllClassrooms(); // Story 14
    }
}
