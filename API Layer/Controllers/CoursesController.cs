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
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
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
        public async Task<ActionResult<ServiceResponse<Course>>> CourseAssignToTeacher(int departmentId, int teacherId, string code)
        {
            var response = await _service.GetByCompositeKey(departmentId, code);
            
            if (response.Success == false) return BadRequest(response);

            if (response.Data == null)
            {
                return BadRequest(response);
            }
            
            if(response.Data.TeacherId == teacherId)
            {
                response.Message = $"The course {code} of the department is aleady assigned to the same teacher! :)";
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
            response.Data.TeacherId = teacherId;
            var response2 = await _service.Update(response.Data);
            if (response2.Success == false) return BadRequest(response2);

            response.Message = $"Course successfully assigned to respective teacher.";
            return Ok(response);
        }
    }
}
