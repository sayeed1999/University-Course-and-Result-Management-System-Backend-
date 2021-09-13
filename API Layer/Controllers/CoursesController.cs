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
        [HttpGet("IncludeTeachersAndSemisters/Department/{departmentId}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetCoursesByDepartmentIncludingTeachersAndSemisters(long departmentId)
        {
            var serviceResponse = await _service.GetCoursesByDepartmentWithTeacherAndSemister(departmentId);
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
        public async Task<ActionResult<ServiceResponse<IEnumerable<ClassSchedule>>>> GetCoursesWithAllocatedRoomsByDepartment(long departmentId)
        {
            var response = await _service.GetClassScheduleByDepartment(departmentId);
            if (response.Success == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("Student/{studentId}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetEnrolledCoursesByStudent(long studentId)
        {
            var response = await _service.GetEnrolledCoursesByStudent(studentId);
            if (response.Success == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("UnassignAll")]
        public async Task<ActionResult> UnassignAllCourses()
        {
            var unassignCourses = await _service.UnassignAllCourses();
            if (unassignCourses.Success == false) return BadRequest(unassignCourses);

            return Ok(unassignCourses);
        }
    }
}
