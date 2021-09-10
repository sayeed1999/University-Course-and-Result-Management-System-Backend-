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
        private readonly IDepartmentRepository service;
        private readonly IUnitOfWork<ApplicationDbContext> unitOfWork;

        public DepartmentService()
        {
            this.unitOfWork = new UnitOfWork<ApplicationDbContext>();
            this.service = new DepartmentRepository(unitOfWork);
        }

        // Story 01
        public async Task<ServiceResponse<Department>> SaveDepartment(Department department)
        {
            var serviceResponse = new ServiceResponse<Department>();
            serviceResponse.Data = department;
            try
            {
                //unitOfWork.CreateTransaction();
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
                var tempResponse = await service.GetDepartmentByCode(department.Code);
                if(tempResponse.Data != null)
                {
                    error += "Code is duplicate.\n";
                }
                tempResponse = await service.GetDepartmentByName(department.Name);
                if(tempResponse.Data != null)
                {
                    error += "Name is duplicate.\n";
                }
                if(error.Length > 0)
                {
                    throw new Exception(error);
                }
                serviceResponse = await service.Add(department);
                // operations end
                //unitOfWork.Save();
                //unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
                //unitOfWork.Rollback();
            }
            return serviceResponse;
        }

        // Story 02
        public async Task<ServiceResponse<IEnumerable<Department>>> GetDepartments()
        {
            return await service.GetAll();      
        }

    }
}
