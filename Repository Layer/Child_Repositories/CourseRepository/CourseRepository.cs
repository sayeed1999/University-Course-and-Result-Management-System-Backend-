using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<ServiceResponse<Course>> GetCourseByCode(string code)
        {
            var serviceResponse = new ServiceResponse<Course>();
            serviceResponse.Data = await _dbSet.SingleOrDefaultAsync(x => x.Code == code);
            return serviceResponse;
        }

        public async Task<ServiceResponse<Course>> GetCourseByName(string name)
        {
            var serviceResponse = new ServiceResponse<Course>();
            serviceResponse.Data = await _dbSet.SingleOrDefaultAsync(x => x.Name == name);
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(long departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbSet.Where(x => x.DepartmentId == departmentId)
                                                   .ToListAsync();
                serviceResponse.Message = "Data fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentWithTeacherAndSemister(long departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbSet.Where(x => x.DepartmentId == departmentId)
                                                   .Include(x => x.Teacher)
                                                   .Include(x => x.Semister)
                                                   .ToListAsync();

                serviceResponse.Message = "Data fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Some error occurred while fetching data.\nError message: " + ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }


        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesWithAllocatedRoomsByDepartment(long departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = from course in _dbSet
                                       where course.DepartmentId == departmentId
                                       select new Course
                                       {
                                           Code = course.Code,
                                           Name = course.Name,
                                           AllocateClassrooms = (ICollection<AllocateClassroom>)
                                                                _dbContext.AllocateClassrooms
                                                                          .Where(x => x.CourseId == course.Id)
                                                                          .Include(x => x.Room)
                                                                          .Include(x => x.Day)
                                       };
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> GetEnrolledCoursesByStudent(long studentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            var courses = new List<Course>();
            foreach(var studentCourse in _dbContext.StudentsCourses.Include(x => x.Course))
            {
                if(studentCourse.StudentId == studentId)
                {
                    courses.Add(studentCourse.Course);
                }
            }
            serviceResponse.Data = courses;
            return serviceResponse;
        }

        public async Task<bool> IsCourseInDepartment(long courseId, long departmentId)
        {
            Course course = await _dbSet.FindAsync(courseId);
            bool ret = false;
            if(course != null)
            {
                if(course.DepartmentId == departmentId)
                {
                    ret = true;
                }
            }
            return ret;
        }

        public async Task<ServiceResponse<IEnumerable<Course>>> UnassignAllCourses()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();

            try
            {
                var count = await _dbContext.CoursesHistories.CountAsync();
                long unassignId = 0;
                if(count == 0)
                {
                    unassignId = 1;
                }
                else
                {
                    var temp = await _dbContext.CoursesHistories
                                            .OrderByDescending(x => x.Id)
                                            .FirstOrDefaultAsync();
                    unassignId = temp.NthHistory + 1;
                }

                List<Course> courses = await _dbContext.Courses.ToListAsync();
                foreach (Course course in courses)
                {
                    CourseHistory courseHistory = new CourseHistory { Code = course.Code, DepartmentId = course.DepartmentId, SemisterId = course.SemisterId, TeacherId = course?.TeacherId, NthHistory = unassignId };
                    await _dbContext.CoursesHistories.AddAsync(courseHistory);
                    course.TeacherId = null;
                    _dbContext.Courses.Update(course);
                }
                
                var studentsCourses = await _dbContext.StudentsCourses.ToListAsync();
                foreach(var studentCourse in studentsCourses)
                {
                    var newStudentCourse = new StudentCourseHistory { DepartmentId = studentCourse.DepartmentId, CourseId = studentCourse.CourseId, Date = studentCourse.Date, StudentId = studentCourse.StudentId, GradeId = studentCourse.GradeId, NthHistory = unassignId };
                    await _dbContext.StudentCourseHistories.AddAsync(newStudentCourse);
                    _dbContext.StudentsCourses.Remove(studentCourse);
                }

                serviceResponse.Message = "Courses & Students History successfully saved!";
            
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Unassigning courses and students failed. :(";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
    }
}
