using Entity_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Repository_Layer.Child_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeRepository service;

        public GradesController(IGradeRepository service)
        {
            this.service = service;
        }

        // GET: Grades
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Student>>> GetGrades()
        {
            var serviceResponse = await service.GetAll();
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

    }
}
