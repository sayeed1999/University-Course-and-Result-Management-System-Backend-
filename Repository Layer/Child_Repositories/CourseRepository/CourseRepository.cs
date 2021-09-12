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

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentWithTeacher(long departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbSet.Where(x => x.DepartmentId == departmentId)
                                                   .Include(x => x.Teacher)
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

        /*
        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentIncludingTeachersAndSemisters(long departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbContext.Courses
                    .Include(x => x.Teacher)
                    .Include(x => x.Semister)
                    .Where(x => x.DepartmentId == departmentId)
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

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(string departmentCode)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            var dept = await _dbContext.Departments.SingleOrDefaultAsync(x => x.Code == departmentCode);
            
            if(dept == null)
            {
                serviceResponse.Message = "Department not found with the code";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            return await this.GetCoursesByDepartment(dept.Id);
        }
        */

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

        /*
        public async Task<ServiceResponse<List<CourseHistory>>> UnassignAllCourses()
        {
            var serviceResponse = new ServiceResponse<List<CourseHistory>>();
            serviceResponse.Data = new List<CourseHistory>();
            try
            {
                var count = await _dbContext.CoursesHistories.CountAsync();
                int unassignId = 0;
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
                    serviceResponse.Data.Add(courseHistory);
                    _dbContext.CoursesHistories.Add(courseHistory);
                    if(course.TeacherId != null)
                    {
                        try
                        {
                            Teacher teacher = await _dbContext.Teachers.FindAsync(course.TeacherId);
                            teacher.RemainingCredit = teacher.CreditToBeTaken;
                            _dbContext.Teachers.Update(teacher);
                        }
                        catch (Exception ex)
                        {
                            serviceResponse.Message = "Fatal Error! Execution stopped at the middle";
                            serviceResponse.Success = false;
                            return serviceResponse;
                        }
                    }

                    course.TeacherId = null;
                    _dbContext.Courses.Update(course);
                }
                await _dbContext.SaveChangesAsync();
                
                var studentsCourses = await _dbContext.StudentsCourses.ToListAsync();
                foreach(var studentCourse in studentsCourses)
                {
                    var newStudentCourse = new StudentCourseHistory { DepartmentId = studentCourse.DepartmentId, CourseCode = studentCourse.CourseCode, Date = studentCourse.Date, StudentId = studentCourse.StudentId, Grade = studentCourse.Grade, NthHistory = unassignId };
                    _dbContext.StudentCourseHistories.Add(newStudentCourse);
                    _dbContext.StudentsCourses.Remove(studentCourse);
                }

                serviceResponse.Message = "Courses & Students History successfully saved!";
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Fatal error! Course saving failed. May be you need to clear data manually in the db";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }*/
    }
}
