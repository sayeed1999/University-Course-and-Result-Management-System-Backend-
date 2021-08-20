using Entity_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.CourseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        // GET: Courses
        [HttpGet("IncludeTeachersAndSemisters/Department/{departmentId:int}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetCoursesByDepartmentIncludingTeachersAndSemisters(int departmentId)
        {
            var serviceResponse = await _service.GetCoursesByDepartmentIncludingTeachersAndSemisters(departmentId);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Courses
        [HttpGet("IncludeTeachers/Department/{departmentId:int}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetCoursesByDepartmentIncludingTeachers(int departmentId)
        {
            var serviceResponse = await _service.GetCoursesByDepartmentIncludingTeachers(departmentId);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Courses
        [HttpGet("Department/{departmentId:int}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetCoursesByDepartment(int departmentId)
        {
            var serviceResponse = await _service.GetCoursesByDepartment(departmentId);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // POST: Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Course>>> PostCourse(Course course)
        {
            var serviceResponse = await _service.Add(course);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpPost("CourseAssignToTeacher")]
        public async Task<ActionResult<ServiceResponse<Course>>> CourseAssignToTeacher([FromBody]CourseAssignToTeacher body)
        {
            var response = await _service.GetByCompositeKey(body.DepartmentId, body.CourseCode);
            
            if (response.Success == false) return BadRequest(response);

            if (response.Data == null)
            {
                return BadRequest(response);
            }
            
            if(response.Data.TeacherId == body.TeacherId)
            {
                response.Message = $"The course {body.CourseCode} of the department is aleady assigned to the same teacher! :)";
                response.Success = false;
                return BadRequest(response);
            }

            // if(response.Data.Teacher.RemainingCredit < response.Data.Credit) client hasn't asked for it in backend!
            // { }

            if(response.Data.TeacherId != null)
            {
                response.Message = $"This course is already assigned to {response.Data.Teacher.Name}! :)";
                response.Success = false;
                return BadRequest(response);
            }
            // else, now assign
            response.Data.TeacherId = body.TeacherId;
            var response2 = await _service.Update(response.Data);
            if (response2.Success == false) return BadRequest(response2);

            response.Message = $"Course successfully assigned to respective teacher.";
            return Ok(response);
        }
    }
}
