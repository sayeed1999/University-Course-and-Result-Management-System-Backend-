using Entity_Layer;
using Repository_Layer;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Story 03
        public async Task<ServiceResponse<Course>> SaveCourse(Course course)
        {
            var serviceResponse = new ServiceResponse<Course>();
            serviceResponse.Data = course;
            try
            {
                //unitOfWork.CreateTransaction();
                string error = "";
                // operations start
                if (course.Code == null || course.Name == null)
                {
                    error = "Model is invalid";
                    throw new Exception(error);
                }
                if (course.Code.Length < 5)
                {
                    error += "\nCode must be atleast 5 characters.";
                }
                if(course.Credit < 0.5 || course.Credit > 5.0)
                {
                    error += "\nCredit must be between 0.5 - 5.0.";
                }
                if(course.DepartmentId <= 0)
                {
                    error += "\nA valid department should be chosen";
                }
                if (course.SemisterId <= 0)
                {
                    error += "\nA valid semester should be chosen";
                }
                var tempResponse = await _unitOfWork.Courses.GetCourseByCode(course.Code);
                if (tempResponse.Data != null)
                {
                    error += "\nCode is duplicate.";
                }
                tempResponse = await _unitOfWork.Courses.GetCourseByName(course.Name);
                if (tempResponse.Data != null)
                {
                    error += "\nName is duplicate.";
                }
                if (error.Length > 0)
                {
                    throw new Exception(error);
                }
                serviceResponse = await _unitOfWork.Courses.Add(course);
                // operations end
                await _unitOfWork.CompleteAsync(); // if it fails in the middle, it should automatically rollback...
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Course>> UpdateCourse(Course course)
        {
            var serviceResponse = new ServiceResponse<Course>();
            try
            {
                serviceResponse = await _unitOfWork.Courses.Update(course);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Operation unsuccessful";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Course>> GetCourseById(long courseId)
        {
            return await _unitOfWork.Courses.GetById(courseId);
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(long departmentId)
        {
            return await _unitOfWork.Courses.GetCoursesByDepartment(departmentId);
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentWithTeacher(long departmentId)
        {
            return await _unitOfWork.Courses.GetCoursesByDepartmentWithTeacher(departmentId);
        }

        public async Task<ServiceResponse<IEnumerable<ClassSchedule>>> GetClassScheduleByDepartment(long departmentId)
        {
            ServiceResponse<IEnumerable<Course>> coursesResponse = await _unitOfWork.Courses.GetCoursesWithAllocatedRoomsByDepartment(departmentId);
            var response = new ServiceResponse<IEnumerable<ClassSchedule>>();
            var schedule = new List<ClassSchedule>();
            foreach(var course in coursesResponse.Data)
            {
                StringBuilder scheduleInfo = new StringBuilder();
                foreach(var tmp in course.AllocateClassrooms)
                {
                    if (scheduleInfo.Length > 0)
                    {
                        scheduleInfo.Append(';');
                        scheduleInfo.AppendLine();
                    }
                    string from = tmp.From < 13.00 ? $"{String.Format("{0:.00}", tmp.From)} AM" : $"{String.Format("{0:.00}", (tmp.From - 12.00))} PM";
                    from = from.Replace('.', ':');
                    string to = tmp.To < 13 ? $"{String.Format("{0:.00}", tmp.To)} AM" : $"{String.Format("{0:.00}", (tmp.To - 12.00))} PM";
                    to = to.Replace('.', ':');
                    scheduleInfo.Append($"R. No : {tmp.Room.Name}, {tmp.Day.Name}, {from} - {to}");
                }
                schedule.Add(new ClassSchedule
                {
                    Code = course.Code,
                    Name = course.Name,
                    ScheduleInfo = scheduleInfo.ToString(),
                });
            }
            response.Data = schedule;
            return response;
        }
    }
}
