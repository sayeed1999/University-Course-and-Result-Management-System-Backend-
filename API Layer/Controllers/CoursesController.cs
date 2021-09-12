using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
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

        public CoursesController(ICourseService courseService)
        {
            this._service = courseService;
        }
        /*
        // GET: Courses
        [HttpGet("IncludeTeachersAndSemisters/Department/{departmentId:int}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetCoursesByDepartmentIncludingTeachersAndSemisters(int departmentId)
        {
            var serviceResponse = await _service.GetCoursesByDepartmentIncludingTeachersAndSemisters(departmentId);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Courses
        [HttpGet("Department/{code}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetCoursesByDepartment(string code)
        {
            var serviceResponse = await _service.GetCoursesByDepartment(code);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }
        */
        // POST: Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Course>>> PostCourse(Course course)
        {
            var serviceResponse = await _service.SaveCourse(course);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Courses
        [HttpGet("Department/{departmentId}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetCoursesByDepartment(long departmentId)
        {
            var serviceResponse = await _service.GetCoursesByDepartment(departmentId);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        // GET: Courses
        [HttpGet("IncludeTeachers/Department/{departmentId}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetCoursesByDepartmentIncludingTeachers(long departmentId)
        {
            var serviceResponse = await _service.GetCoursesByDepartmentWithTeacher(departmentId);
            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpPost("CourseAssignToTeacher")]
        public async Task<ActionResult<ServiceResponse<Course>>> CourseAssignToTeacher([FromBody]CourseAssignToTeacher body)
        {
            ServiceResponse<Course> response = await _service.GetCourseById(body.CourseId);
            
            if (response.Data == null)
            {
                response.Message = "Course not found on the database.";
                return BadRequest(response);
            }
            
            if(response.Data.TeacherId == body.TeacherId)
            {
                response.Message = $"This course is already assigned to the same teacher. :)";
                response.Success = false;
                return BadRequest(response);
            }

            // if(response.Data.Teacher.RemainingCredit < response.Data.Credit) client hasn't asked for it in backend!
            // { }

            if(response.Data.TeacherId != null)
            {
                response.Message = $"This course is already assigned to another teacher. :(";
                response.Success = false;
                return BadRequest(response);
            }
            
            // else, now assign teacher
            response.Data.TeacherId = body.TeacherId;
            response = await _service.UpdateCourse(response.Data);
            if (response.Success == false) return BadRequest(response);

            response.Message = $"Course successfully assigned to respective teacher.";
            return Ok(response);
        }
        
        [HttpGet("Department/{departmentId:int}/ClassSchedule")]
        public async Task<ActionResult<IEnumerable<ServiceResponse<ClassSchedule>>>> GetCoursesWithAllocatedRoomsByDepartment(long departmentId)
        {
            var response = await _service.GetClassScheduleByDepartment(departmentId);
            if (response.Success == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("Student/{studentId}")]
        public async Task<ActionResult<IEnumerable<ServiceResponse<ClassSchedule>>>> GetEnrolledCoursesByStudent(long studentId)
        {
            var response = await _service.GetEnrolledCoursesByStudent(studentId);
            if (response.Success == false) return BadRequest(response);
            return Ok(response);
        }

        /*
        [HttpDelete("UnassignAll")]
        public async Task<ActionResult> UnassignAllCourses()
        {
            var unassignCourses = await _service.UnassignAllCourses();
            if (unassignCourses.Success == false) return BadRequest(unassignCourses);

            return Ok(unassignCourses);
        }*/
    }
}
