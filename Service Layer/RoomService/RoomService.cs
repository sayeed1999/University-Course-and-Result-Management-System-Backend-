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

            if (data.CourseId <= 0 || data.DayId <= 0 || data.From < 0 || data.From >= 24 || data.To < 0 || data.To >= 24)
            {
                response.Success = false;
                response.Message = "Model is invalid";
                return response;
            }

            if((data.From > (Math.Floor(data.From) + 0.59)) || (data.To > (Math.Floor(data.To) + 0.59)))
            {
                response.Success = false;
                response.Message = "Second cannot be greater than 59.";
                return response;
            }

            if (data.From >= data.To)
            {
                response.Message = "Class duration cannot be less than zero minute or negative time.";
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
