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
            var response = new ServiceResponse<IEnumerable<Room>>();
            try
            {
                response.Data = await _unitOfWork.RoomRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Room fetching failed. :(";
                response.Success = false;
            }
            return response;
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

            if((data.From > (float)(Math.Floor(data.From) + 0.59)) || (data.To > (float)(Math.Floor(data.To) + 0.59)))
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

            long count = await _unitOfWork.AllocateClassroomRepository
                                   .CountAsync(x => x.RoomId == data.RoomId
                                            && x.DayId == data.DayId
                                            && (x.From < data.To && x.To > data.From)
                                   );
            if (count > 0)
            {
                response.Message = "Overlap in classes occurred. Check class schedule please.";
                response.Success = false;
                return response;
            }
            
            try
            {
                await _unitOfWork.AllocateClassroomRepository.AddAsync(data);
                await _unitOfWork.CompleteAsync();
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<AllocateClassroom>>> UnallocateAllClassrooms()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<AllocateClassroom>>();

            long nthUnallocating = 0;
            long count = await _unitOfWork.AllocateClassroomHistoryRepository.CountAsync();
            if (count == 0)
            {
                nthUnallocating = 1;
            }
            else
            {
                var temp = await _unitOfWork.AllocateClassroomHistoryRepository.LastOrDefaultAsync();
                nthUnallocating = temp.NthHistory + 1; // this is the nth time you are unallocating classrooms...
            }

            IEnumerable<AllocateClassroom> allocatedRooms = await _unitOfWork.AllocateClassroomRepository.ToListAsync();
            foreach (var room in allocatedRooms)
            {
                AllocateClassroomHistory roomHistory = new AllocateClassroomHistory { CourseId = room.CourseId, DayId = room.DayId, DepartmentId = room.DepartmentId, From = room.From, To = room.To, RoomId = room.RoomId, NthHistory = nthUnallocating };
                await _unitOfWork.AllocateClassroomHistoryRepository.AddAsync(roomHistory);
                _unitOfWork.AllocateClassroomRepository.Delete(room);
            }

            try
            {
                await _unitOfWork.CompleteAsync();
                serviceResponse.Message = "Allocated Rooms History successfully saved!";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Ünallocating classrooms failed.";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
    }
}
