using Entity_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.StudentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            this._service = service;
        }

        // GET: Students
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Student>>> GetStudents()
        {
            var serviceResponse = await _service.GetAll();
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        // POST: Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Student>>> PostStudent(StudentRegistration student)
        {
            var serviceResponse = await _service.RegisterStudent(student);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpPost("enroll-student-in-course")]
        public async Task<ActionResult<ServiceResponse<StudentCourse>>> EnrollStudentInCourse([FromBody] StudentCourse data)
        {
            var serviceResponse = await _service.EnrollStudentInCourse(data);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }
    }
}
