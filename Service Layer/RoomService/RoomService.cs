using Entity_Layer;
using Repository_Layer;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<Room>>> GetRooms()
        {
            return await _unitOfWork.Rooms.GetAll();
        }

        public async Task<ServiceResponse<AllocateClassroom>> AllocateClassroom(AllocateClassroom data)
        {
            var response = new ServiceResponse<AllocateClassroom>();
            response.Data = data;

            if (data.From >= data.To)
            {
                response.Message = "Class duration cannot be less than one minute!";
                response.Success = false;
                return response;
            }

            try
            {
                response = await _unitOfWork.Rooms.AllocateClassroom(data);
                await _unitOfWork.CompleteAsync();
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
