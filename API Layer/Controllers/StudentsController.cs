using Data_Access_Layer;
using Entity_Layer;
using FastReport.Export.PdfSimple;
using FastReport.Data;
using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IO;
using Service_Layer.StudentService;

namespace API_Layer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly AppSettings _appSettings;
        public StudentsController(IStudentService service, IOptions<AppSettings> appSettings)
        {
            this._service = service;
            this._appSettings = appSettings.Value;
        }
        
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Student>>>> GetStudents(string regNum = "")
        {
            var serviceResponse = await _service.GetAll(regNum);
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpGet]
        [Route("RegistrationNumber/{reg}")]
        public async Task<ActionResult<ServiceResponse<Student>>> GetStudentByRegistrationNumber(string reg)
        {
            var serviceResponse = await _service.GetStudentByRegNum(reg);
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Students/Results/Student/1
        [HttpGet]
        [Route("Results/Student/{id:long}")]
        public async Task<ActionResult<ServiceResponse<Student>>> GetStudentResultById(long id)
        {
            var serviceResponse = await _service.GetStudentResultById(id);
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpGet("Result-Sheet/Student/{id:long}")]
        public async Task<IActionResult> PrintStudentResultByRegistrationNumber(long id)
        {
            var serviceResponse = await _service.GetStudentResultById(id);
            if(serviceResponse.Success)
            {
                var webReport = new WebReport();
                var msSqlDataConnection = new MsSqlDataConnection();
                msSqlDataConnection.ConnectionString = _appSettings.ConnectionString;
                webReport.Report.Dictionary.Connections.Add(msSqlDataConnection);
                webReport.Report.Load("report.frx");
                webReport.Report.SetParameterValue("id", serviceResponse.Data.Id);
                webReport.Report.Prepare(false);
                PDFSimpleExport pdf = new PDFSimpleExport();
                webReport.Report.Export(pdf, "report.pdf");
                var stream = new FileStream("report.pdf", FileMode.Open);
                return new FileStreamResult(stream, "application/pdf");
            }
            return NotFound(serviceResponse);
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
