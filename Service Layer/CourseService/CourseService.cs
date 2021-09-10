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
    }
}
