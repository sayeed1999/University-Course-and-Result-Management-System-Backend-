using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.TeacherService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeachersController(ITeacherService service)
        {
            this._service = service;
        }

        // POST: Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Teacher>>> PostTeacher(Teacher teacher)
        {
            if (teacher == null)
            {
                var response = new ServiceResponse<Teacher>();
                response.Message = "Model is null";
                response.Success = false;
                return BadRequest(response);
            }
            var serviceResponse = await _service.SaveTeacher(teacher);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpGet("Department/{departmentId}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Teacher>>>> GetTeachersByDepartmentWithAssignedCourses(long departmentId)
        {
            var serviceResponse = await _service.GetTeachersByDepartmentWithAssignedCourses(departmentId);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }
    }
}
