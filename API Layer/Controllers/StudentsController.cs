using Data_Access_Layer;
using Entity_Layer;
using FastReport.Export.PdfSimple;
using FastReport.Data;
using FastReport.Export.Image;
using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.StudentService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentService _service;
        private readonly AppSettings _appSettings;
        public StudentsController(IStudentService service, IOptions<AppSettings> appSettings)
        {
            this._service = service;
            this._appSettings = appSettings.Value;
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

        [AllowAnonymous]
        [HttpGet("Result-Sheet/{reg}")]
        public async Task<IActionResult> PrintStudentResultByRegistrationNumber(string reg)
        {
            var serviceResponse = await _service.GetStudentResultByRegNo(reg);

            if(serviceResponse.Success)
            {
                var webReport = new WebReport();
                var msSqlDataConnection = new MsSqlDataConnection();
                msSqlDataConnection.ConnectionString = _appSettings.ConnectionString;
                webReport.Report.Dictionary.Connections.Add(msSqlDataConnection);
                webReport.Report.Load("report.frx");
                webReport.Report.SetParameterValue("reg", reg);
                webReport.Report.Prepare();
                PDFSimpleExport pdf = new PDFSimpleExport();
                webReport.Report.Export(pdf, "report.pdf");
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
