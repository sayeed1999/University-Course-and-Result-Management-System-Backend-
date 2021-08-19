using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ServiceResponse<AllocateClassroom>> AllocateClassroom(AllocateClassroom data)
        {
            var response = new ServiceResponse<AllocateClassroom>();
            response.Data = data;
            var count = await _dbContext.AllocateClassrooms.CountAsync(
                                                            x => x.RoomId == data.RoomId
                                                        && x.DayId == data.DayId
                                                        && ( ( x.From.Hour < data.To.Hour || (x.From.Hour == data.To.Hour && x.From.Minute < data.To.Minute) )
                                                        || (x.To.Hour > data.From.Hour || (x.To.Hour == data.From.Hour && x.To.Minute > data.From.Minute) ) )
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
    }
}
