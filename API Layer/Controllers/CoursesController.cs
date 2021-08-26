using Entity_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.CourseService;
using Service_Layer.TeacherService;
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
        private readonly ITeacherService _teacherService;

        public CoursesController(ICourseService service, ITeacherService service2)
        {
            _service = service;
            _teacherService = service2;
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
        [HttpGet("Department/{id:int}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Course>>>> GetCoursesByDepartment(int id)
        {
            var serviceResponse = await _service.GetCoursesByDepartment(id);
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
            var response = await _service.GetCourseByCompositeKeyIncludingTeacher(body.DepartmentId, body.CourseCode);
            //return Ok(response);
            if (response.Success == false) return BadRequest(response);

            if (response.Data == null)
            {
                response.Message = "Course not found on the database.";
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
            
            // else, now assign teacher
            response.Data.TeacherId = body.TeacherId;
            var response2 = await _service.Update(response.Data);
            if (response2.Success == false) return BadRequest(response2);

            // now teacher is assigned, reduce the remainingCredit
            var teacherResponse = await _teacherService.GetById(body.TeacherId);
            
            if(teacherResponse.Success == false)
            {
                teacherResponse.Message = "Non-reversible error! Course Assigned to teacherId that doesn't exist! You have to manually change it or unassign the teacher. :(";
                return BadRequest(teacherResponse);
            }

            Teacher teacher = teacherResponse.Data;
            teacher.RemainingCredit = teacher.RemainingCredit <= response.Data.Credit ? 0 : teacher.RemainingCredit - response.Data.Credit;

            teacherResponse = await _teacherService.Update(teacher);
            if (teacherResponse.Success == false)
            {
                teacherResponse.Message = "Non-reversible error! Course Assigned but remaining credit from teacher not reduced. You have to manually change it or unassign the teacher. :(";
                return BadRequest(teacherResponse);
            }

            response.Message = $"Course successfully assigned to respective teacher.";
            return Ok(response);
        }

        [HttpGet("Department/{departmentId:int}/AllocatedRooms")]
        public async Task<ActionResult<IEnumerable<ServiceResponse<Course>>>> GetCoursesWithAllocatedRoomsByDepartment(int departmentId)
        {
            var response = await _service.GetCoursesWithAllocatedRoomsByDepartment(departmentId);
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
