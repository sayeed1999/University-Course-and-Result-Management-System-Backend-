using Entity_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.SemisterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SemistersController : ControllerBase
    {
        private readonly ISemisterService _service;
        public SemistersController(ISemisterService service)
        {
            _service = service;
        }

        // GET: Semisters
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Semister>>>> GetSemisters()
        {
            var serviceResponse = await _service.GetAll();
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }
    }
}
