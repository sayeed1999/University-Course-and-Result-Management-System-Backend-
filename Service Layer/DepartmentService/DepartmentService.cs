using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository_Layer;
using Repository_Layer.Child_Repositories;
using Repository_Layer.Repository;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Story 01
        public async Task<ServiceResponse<Department>> SaveDepartment(Department department)
        {
            var serviceResponse = new ServiceResponse<Department>();
            serviceResponse.Data = department;
            try
            {
                string error = "";
                // operations start
                if(department.Code == null || department.Name == null)
                {
                    error = "Model is invalid";
                    throw new Exception(error);
                }
                if (department.Code.Length < 2 && department.Code.Length > 7)
                {
                    error += "Code must be between 2-7 characters.\n";
                }
                try
                {
                    Department dept = await _unitOfWork.DepartmentRepository.SingleOrDefaultAsync(x => x.Code == department.Code);
                    if(dept != null)
                    {
                        error += "Code is duplicate.\n";
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                try
                {
                    Department dept = await _unitOfWork.DepartmentRepository.SingleOrDefaultAsync(x => x.Name == department.Name);
                    if (dept != null)
                    {
                        error += "Name is duplicate.\n";
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                if(error.Length > 0)
                {
                    throw new Exception(error);
                }
                await _unitOfWork.DepartmentRepository.AddAsync(department);
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

        // Story 02
        public async Task<ServiceResponse<IEnumerable<VIEW_Department>>> ViewDepartments()
        {
            var response = new ServiceResponse<IEnumerable<VIEW_Department>>();
            try
            {
                response.Data = await _unitOfWork.DepartmentRepository.ViewAllDepartmentsAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Department fetching failed. :(";
                response.Success = false;
            }
            return response;
        }

        // Story 02
        public async Task<ServiceResponse<IEnumerable<Department>>> GetDepartments()
        {
            var response = new ServiceResponse<IEnumerable<Department>>();
            try
            {
                response.Data = await _unitOfWork.DepartmentRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Department fetching failed. :(";
                response.Success = false;
            }
            return response;
        }
    }
}
