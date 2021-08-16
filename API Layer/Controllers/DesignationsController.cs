using Entity_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.DesignationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesignationsController : ControllerBase
    {
        private readonly IDesignationService _service;

        public DesignationsController(IDesignationService service)
        {
            this._service = service;
        }

        // GET: Designations
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Designation>>>> GetDesignations()
        {
            var serviceResponse = await _service.GetAll();
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

    }
}
