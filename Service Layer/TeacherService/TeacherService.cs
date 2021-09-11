using Entity_Layer;
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
                //unitOfWork.CreateTransaction();
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
                var tempResponse = await _unitOfWork.Teachers.GetTeacherByEmail(teacher.Email);
                if (tempResponse.Data != null)
                {
                    error += "Duplicate email found.\n";
                }
                if (error.Length > 0)
                {
                    throw new Exception(error);
                }
                serviceResponse = await _unitOfWork.Teachers.Add(teacher);
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
            return await _unitOfWork.Teachers.GetTeachersByDepartmentWithAssignedCourses(departmentId);
        }
    }
}
