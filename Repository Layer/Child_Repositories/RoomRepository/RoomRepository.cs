using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace Repository_Layer.Child_Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ServiceResponse<AllocateClassroom>> AllocateClassroom(AllocateClassroom data)
        {
            var response = new ServiceResponse<AllocateClassroom>();
            response.Data = data;
            
            if(data.From >= data.To)
            {
                response.Message = "Class duration cannot be less than one minute!";
                response.Success = false;
                return response;
            }

            var count = await _dbContext.AllocateClassrooms.CountAsync(
                                                            x => x.RoomId == data.RoomId
                                                        && x.DayId == data.DayId
                                                        && (x.From < data.To && x.To > data.From)
                                                      );
            if ( count > 0)
            {
                response.Message = "Some overlap occurred.  Check your routine to avoid overlapping!";
                response.Success = false;
                return response;
            }
            try
            {
                _dbContext.AllocateClassrooms.Add(data);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<AllocateClassroom>>> GetAllocatedRoomsByDepartment(int departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<AllocateClassroom>>();
            try
            {
                serviceResponse.Data = await _dbContext.AllocateClassrooms
                                        
                                        .Where(x => x.DepartmentId == departmentId)
                                        .Include(x => x.Course)
                                        .ToListAsync();
                serviceResponse.Message = "Data fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Some error occurred while fetching data.\nError message: " + ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<AllocateClassroomHistory>>> UnallocateAllClassrooms()
        {
            var serviceResponse = new ServiceResponse<List<AllocateClassroomHistory>>();
            serviceResponse.Data = new List<AllocateClassroomHistory>();
            try
            {
                long nthUnallocating = 0;
                if(await _dbContext.AllocateClassroomHistories.CountAsync() == 0)
                {
                    nthUnallocating = 1;
                }
                else
                {
                    var temp = await _dbContext.AllocateClassroomHistories.OrderByDescending(x => x.Id)
                                    .FirstOrDefaultAsync();
                    nthUnallocating = temp.NthHistory + 1; // this is the nth time you are unallocating classrooms...
                }

                List<AllocateClassroom> allocatedRooms = await _dbContext.AllocateClassrooms.ToListAsync();
                foreach (var room in allocatedRooms)
                {
                    AllocateClassroomHistory roomHistory = new AllocateClassroomHistory { CourseCode = room.CourseCode, DayId = room.DayId, DepartmentId = room.DepartmentId, From = room.From, To = room.To, RoomId = room.RoomId, NthHistory = nthUnallocating };
                    serviceResponse.Data.Add(roomHistory);
                    _dbContext.AllocateClassroomHistories.Add(roomHistory);
                    _dbContext.AllocateClassrooms.Remove(room);
                }
                await _dbContext.SaveChangesAsync();
                serviceResponse.Message = "Allocated Rooms History successfully saved!";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Fatal error! Course saving failed. May be you need to clear data manually in the db";
                serviceResponse.Success = false;
            }
            return serviceResponse;

        }
    }
}
*/