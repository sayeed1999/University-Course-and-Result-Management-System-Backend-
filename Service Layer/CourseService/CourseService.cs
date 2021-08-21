﻿using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.CourseService
{
    public class CourseService : Repository<Course>, ICourseService
    {
        public CourseService(ApplicationDbContext dbContext) : base(dbContext) { }

        public override async Task<ServiceResponse<Course>> Add(Course item)
        {
            var serviceResponse = new ServiceResponse<Course>();

            // find if there remains a course with the same name in the same department
            if (await _dbContext.Courses.SingleOrDefaultAsync(x => (x.Code == item.Code || x.Name == item.Name) && x.DepartmentId == item.DepartmentId) != null)
            {
                serviceResponse.Data = item;
                serviceResponse.Message = "Code and name must be unique in the respective department";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            try
            {
                _dbContext.Courses.Add(item);
                await _dbContext.SaveChangesAsync();
                serviceResponse.Data = item;
                serviceResponse.Message = "Item stored successfully to the database.";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public virtual async Task<ServiceResponse<Course>> GetCourseByCompositeKeyIncludingTeacher(int departmentId, string courseCode)
        {
            var serviceResponse = new ServiceResponse<Course>();
            try
            {
                serviceResponse.Data = await _dbContext.Courses.Include(x => x.Teacher)
                    .SingleOrDefaultAsync(x => x.DepartmentId == departmentId 
                                            && x.Code == courseCode);
                
                if (serviceResponse.Data == null)
                {
                    serviceResponse.Message = "Data not found with the given constrain.";
                    serviceResponse.Success = false;
                }
                else serviceResponse.Message = "Data  with the given id was fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Some error occurred while fetching data.\nError message: " + ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentIncludingTeachersAndSemisters(int departmentId)
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

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartmentIncludingTeachers(int departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbContext.Courses
                    .Include(x => x.Teacher)
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

        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesByDepartment(int departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbContext.Courses
                    .Where(x => x.DepartmentId == departmentId)
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


        public async Task<ServiceResponse<IEnumerable<Course>>> GetCoursesWithAllocatedRoomsByDepartment(int departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Course>>();
            try
            {
                serviceResponse.Data = await _dbContext.Courses.Where(x => x.DepartmentId == departmentId)
                                                            .Include(x => x.AllocateClassrooms)
                                                            .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
    }
}
