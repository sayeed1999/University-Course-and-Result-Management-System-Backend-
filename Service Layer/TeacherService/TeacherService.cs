using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<Teacher>> SaveTeacher(Teacher teacher)
        {
            var serviceResponse = new ServiceResponse<Teacher>();
            serviceResponse.Data = teacher;
            try
            {
                string error = "";
                // operations start
                if (teacher.Name == null || teacher.Address == null || teacher.Email == null)
                {
                    error = "Model is invalid";
                    throw new Exception(error);
                }
                if (teacher.CreditToBeTaken < 0)
                {
                    error += "Credit must be non-negative.\n";
                }
                try
                {
                    Teacher? _teacher = _unitOfWork.TeacherRepository.SingleOrDefault(x => x.Email == teacher.Email);
                    if(_teacher != null)
                    {
                        error += "Duplicate email found.\n";
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
                await _unitOfWork.TeacherRepository.AddAsync(teacher);
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

        public async Task<ServiceResponse<IEnumerable<TeacherView>>> GetTeachersByDepartmentWithAssignedCourses(long departmentId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<TeacherView>>();
            try
            {
                IEnumerable<Teacher> teachers = _unitOfWork.TeacherRepository
                                                                 .Where(x => x.DepartmentId == departmentId)
                                                                 .Include(x => x.Courses)
                                                                 .ToList();
                serviceResponse.Message = "Data fetched successfully from the database";

                var teacherViews = from teacher in teachers
                                   select new TeacherView
                                   {
                                       Address = teacher.Address,
                                       Contact = teacher.Contact,
                                       Courses = teacher.Courses,
                                       CreditToBeTaken = teacher.CreditToBeTaken,
                                       DepartmentId = teacher.DepartmentId,
                                       DesignationId = teacher.DesignationId,
                                       Id = teacher.Id,
                                       Email = teacher.Email,
                                       Name = teacher.Name,
                                       RemainingCredit = teacher.CreditToBeTaken - teacher.Courses.Sum(x => x.Credit)
                                   };
                serviceResponse.Data = teacherViews;
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
