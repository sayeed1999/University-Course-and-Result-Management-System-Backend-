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
        public Task<ServiceResponse<AllocateClassroom>> AllocateClassroom(AllocateClassroom data);
    }
}
