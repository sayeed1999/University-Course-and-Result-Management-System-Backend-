using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.RoomService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService service;

        public RoomsController(IRoomService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Room>>>> GetRooms()
        {
            var serviceResponse = await service.GetRooms();
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        /*[HttpGet("{departmentId:int}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Room>>>> GetAllocatedRoomsByDepartment(int departmentId)
        {
            var serviceResponse = await service.GetAllocatedRoomsByDepartment(departmentId);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }*/

        [HttpPost("allocate-classroom")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Room>>>> AllocateClassroom(AllocateClassroom data)
        {
            data.From = new DateTime(2021, 01, 01, data.From.Hour, data.From.Minute, 00);
            data.To = new DateTime(2021, 01, 01, data.To.Hour, data.To.Minute, 00);

            var serviceResponse = await service.AllocateClassroom(data);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        /*[HttpDelete("UnallocateAll")]
        public async Task<ActionResult<ServiceResponse<List<AllocateClassroomHistory>>>> UnallocateClassrooms()
        {
            var serviceResponse = await service.UnallocateAllClassrooms();
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }*/
    }
}
