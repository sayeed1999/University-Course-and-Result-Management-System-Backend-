using Entity_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.GradeService;
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
        private readonly IGradeService service;

        public GradesController(IGradeService service)
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
