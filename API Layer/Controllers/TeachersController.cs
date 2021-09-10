using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Repository_Layer.Child_Repositories;
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
        private readonly ITeacherRepository _service;

        public TeachersController(ITeacherRepository service)
        {
            this._service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Teacher>>> GetById(int id)
        {
            var serviceResponse = await _service.GetById(id);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // POST: Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Teacher>>> PostTeacher(Teacher teacher)
        {
            //teacher.RemainingCredit = teacher.CreditToBeTaken;
            var serviceResponse = await _service.Add(teacher);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpGet("Department/{departmentId:int}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Teacher>>>> GetTeachersByDepartment(int departmentId)
        {
            var serviceResponse = await _service.GetTeachersByDepartment(departmentId);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }
    }
}
