using Data_Access_Layer;
using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Repository_Layer.Child_Repositories;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _service;
        private readonly IUnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();

        public DepartmentsController()
        {
            _service = new DepartmentRepository(unitOfWork);
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
        public async Task<ActionResult<ServiceResponse<Department>>> PostDepartment(Department department)
        {
            var serviceResponse = await _service.Add(department);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

    }
}
