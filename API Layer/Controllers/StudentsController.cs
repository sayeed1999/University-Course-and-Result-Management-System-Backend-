using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.StudentService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IGeneratePdf _generatePDF;

        public StudentsController(IStudentService service, IGeneratePdf generatePDF)
        {
            this._service = service;
            this._generatePDF = generatePDF;
        }

        // GET: Students
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Student>>>> GetStudents([FromQuery] string regNum = "")
        {
            var serviceResponse = await _service.GetAll(regNum);
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Students/Results
        [HttpGet]
        [Route("Results")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Student>>>> GetStudentsResults()
        {
            var serviceResponse = await _service.GetStudentsResults();
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Students/Results/1
        [HttpGet]
        [Route("Results/{id:long}")]
        public async Task<ActionResult<ServiceResponse<Student>>> GetStudentResultById(long id)
        {
            var serviceResponse = await _service.GetStudentResultById(id);
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Students/Results/MTE-2021-003
        [HttpGet]
        [Route("Results/{reg}")]
        public async Task<ActionResult<ServiceResponse<Student>>> GetStudentResultByRegistrationNumber(string reg)
        {
            var serviceResponse = await _service.GetStudentResultByRegNo(reg);
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        [AllowAnonymous] //fronyend dev can't parse pdf without it
        [HttpGet]
        [Route("Result-Sheet/{reg}")]
        public async Task<IActionResult> PrintStudentResultByRegistrationNumber(string reg)
        {
            var serviceResponse = await _service.GetStudentResultByRegNo(reg);
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return await _generatePDF.GetPdf("Views/ResultSheet.cshtml", serviceResponse.Data);
            //return await _generatePDF.GetByteArray("Views/ResultSheet.cshtml", serviceResponse.Data);
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

        [HttpPost("enroll-in-course")]
        public async Task<ActionResult<ServiceResponse<StudentCourse>>> EnrollStudentInCourse([FromBody] StudentCourse data)
        {
            var serviceResponse = await _service.EnrollStudentInCourse(data);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpPost]
        [Route("save-result")]
        public async Task<ActionResult<ServiceResponse<StudentCourse>>> SaveResult([FromBody] StudentCourse data)
        {
            var serviceResponse = await _service.SaveResult(data);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }
    }
}
