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
    public class DesignationsController : ControllerBase
    {
        private readonly IDesignationRepository _service;

        public DesignationsController(IDesignationRepository service)
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
