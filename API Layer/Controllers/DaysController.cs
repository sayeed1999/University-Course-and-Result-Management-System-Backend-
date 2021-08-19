using Entity_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.DayService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DaysController : ControllerBase
    {
        private readonly IDayService service;

        public DaysController(IDayService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Day>>>> GetDepartments()
        {
            var serviceResponse = await service.GetAll();
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }
    }
}
