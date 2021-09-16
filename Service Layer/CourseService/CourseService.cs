using Entity_Layer;
using Microsoft.EntityFrameworkCore;
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
                try
                {
                    Course? _course = await _unitOfWork.CourseRepository.SingleOrDefaultAsync(x => x.Code == course.Code);
                    if(_course != null)
                    {
                        error += "\nCode is duplicate.";
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                try
                {
                    Course? _course = await _unitOfWork.CourseRepository.SingleOrDefaultAsync(x => x.Name == course.Name);
                    if (_course != null)
                    {
                        error += "\nName is duplicate.";
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                if (error.Length > 0)
                {
                    throw new Exception(error);
                }

                await _unitOfWork.CourseRepository.AddAsync(course);
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
                _unitOfWork.CourseRepository.Update(course);
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
            var serviceResponse = new ServiceResponse<Course>();
            serviceResponse.Data = await _unitOfWork.CourseRepository.FindAsync(courseId);
            if(serviceResponse.Data == null)
            {
                serviceResponse.Message = "Not found";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(long departmentId)
        {
            var response = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                IEnumerable<Course> courses = await _unitOfWork.CourseRepository.Where(x => x.DepartmentId == departmentId)
                                                                                .ToListAsync(); // ei amar where(arrow func.) tolistasync hoyna!
                response.Data = courses;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = "Course fetching failed. :(";
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentWithTeacherAndSemister(long departmentId)
        {
            var response = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                response.Data = await _unitOfWork.CourseRepository.Where(x => x.DepartmentId == departmentId, 
                                                                         x => x.Teacher, 
                                                                         x => x.Semister)
                                                                  .ToListAsync();
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = "Course fetching failed. :(";
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ClassSchedule>>> GetClassScheduleByDepartment(long departmentId)
        {
            IEnumerable<Course> courses = await _unitOfWork.CourseRepository
                                                           .GetByWhereClause(
                                                                x => x.DepartmentId == departmentId, 
                                                                i => i.Include(x => x.AllocateClassrooms)
                                                                      .ThenInclude(x => x.Room),
                                                                i => i.Include(x => x.AllocateClassrooms)
                                                                      .ThenInclude(x => x.Day))
                                                           .ToListAsync();

            var response = new ServiceResponse<IEnumerable<ClassSchedule>>();
            var schedule = new List<ClassSchedule>();
            foreach(var course in courses)
            {
                StringBuilder scheduleInfo = new StringBuilder();

                foreach(var tmp in course.AllocateClassrooms)
                {
                    if (scheduleInfo.Length > 0)
                    {
                        scheduleInfo.Append(';');
                        scheduleInfo.AppendLine();
                    }
                    string from = FormatTime(tmp.From);
                    from = from.Replace('.', ':');
                    string to = FormatTime(tmp.To);
                    to = to.Replace('.', ':');
                    scheduleInfo.Append($"R. No : {tmp.Room.Name}, {tmp.Day.Name}, {from} - {to}");
                }
                schedule.Add(new ClassSchedule
                {
                    Code = course.Code,
                    Name = course.Name,
                    ScheduleInfo = course.AllocateClassrooms.Count > 0 ? scheduleInfo.ToString() : "Not Scheduled Yet",
                });
            }
            response.Data = schedule;
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetEnrolledCoursesByStudent(long studentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                IEnumerable<StudentCourse> studentCourse = await _unitOfWork.StudentCourseRepository
                                                                            .Where(x => x.StudentId == studentId, x => x.Course)
                                                                            .ToListAsync();
                List<Course> courses = new List<Course>();
                foreach (var tmp in studentCourse)
                {
                    courses.Add(tmp.Course);
                }
                serviceResponse.Data = courses;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> UnassignAllCourses()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                long count = await _unitOfWork.CourseHistoryRepository.CountAsync();
                long unassignId = 0;
                if (count == 0)
                {
                    unassignId = 1;
                }
                else
                {
                    var temp = await _unitOfWork.CourseHistoryRepository
                                                .LastOrDefaultAsync();
                    unassignId = (temp != null ? temp.NthHistory : 0) + 1;
                }

                IEnumerable<Course> courses = await _unitOfWork.CourseRepository.Where(x => x.TeacherId != null).ToListAsync();
                foreach (Course course in courses)
                {
                    CourseHistory courseHistory = new CourseHistory { CourseId = course.Id, DepartmentId = course.DepartmentId, SemisterId = course.SemisterId, TeacherId = course?.TeacherId, NthHistory = unassignId };
                    await _unitOfWork.CourseHistoryRepository.AddAsync(courseHistory);
                    course.TeacherId = null;
                    _unitOfWork.CourseRepository.Update(course);
                }

                IEnumerable<StudentCourse> studentsCourses = await _unitOfWork.StudentCourseRepository.ToListAsync();
                foreach (StudentCourse studentCourse in studentsCourses)
                {
                    var newStudentCourse = new StudentCourseHistory { DepartmentId = studentCourse.DepartmentId, CourseId = studentCourse.CourseId, Date = studentCourse.Date, StudentId = studentCourse.StudentId, GradeId = studentCourse.GradeId, NthHistory = unassignId };
                    await _unitOfWork.StudentCourseHistoryRepository.AddAsync(newStudentCourse);
                    _unitOfWork.StudentCourseRepository.Delete(studentCourse);
                }

                serviceResponse.Message = "Courses & Students History successfully saved!";
                // sync..!
                await _unitOfWork.CompleteAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Message = "Unassigning courses and students failed. :(";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        private string FormatTime(float time)
        {
            string ret = "";
            if (time < 0 || time >= 24) throw new Exception("Invalid time!");

            if (time >= 0.00 && time < 1.00)
            {
                time += 12.00f;
                ret = $"{String.Format("{0:0.00}", time)} AM";
            }
            else if (time >= 1.00 && time < 12.00)
            {
                ret = $"{String.Format("{0:0.00}", time)} AM";
            }
            else if (time >= 12.00 && time < 13.00)
            {
                ret = $"{String.Format("{0:0.00}", time)} PM";
            }
            else if (time >= 13.00 && time < 24.00)
            {
                time -= 12.00f;
                ret = $"{String.Format("{0:0.00}", time)} PM";
            }
            return ret;
        }
    }
}
