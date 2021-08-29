using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.DepartmentService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        // GET: Departments
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Department>>>> GetDepartments()
        {
            var serviceResponse = await _service.GetAll();
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Departments/All
        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Department>>>> GetDepartmentsIncludingTeachersAndCourses()
        {
            var serviceResponse = await _service.GetAllIncludingTeachersAndCourses();
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Departments/Courses
        [HttpGet("Courses")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Department>>>> GetDepartmentsIncludingCourses()
        {
            var serviceResponse = await _service.GetAllIncludingCourses();
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // POST: Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<Department>>> PostDepartment(Department department)
        {
            var serviceResponse = await _service.Add(department);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

    }
}
