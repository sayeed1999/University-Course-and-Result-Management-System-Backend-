using Data_Access_Layer;
using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.RoomService
{
    public class RoomService : Repository<Room>, IRoomService
    {
        public RoomService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
