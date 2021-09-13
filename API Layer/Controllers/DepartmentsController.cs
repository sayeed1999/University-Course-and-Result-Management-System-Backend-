using Data_Access_Layer;
using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Repository_Layer.Child_Repositories;
using Repository_Layer.UnitOfWork;
using Service_Layer.DepartmentService;
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
        private readonly IDepartmentService service;

        public DepartmentsController(IDepartmentService service)
        {
            this.service = service;
        }

        // GET: Departments
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Department>>>> GetDepartments()
        {
            var serviceResponse = await service.GetDepartments();
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // POST: Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Department>>> PostDepartment(Department department)
        {
            var serviceResponse = await service.SaveDepartment(department);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

    }
}
