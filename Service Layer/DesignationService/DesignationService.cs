using Entity_Layer;
using Repository_Layer;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.DesignationService
{
    public class DesignationService : IDesignationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DesignationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<Designation>>> GetDesignations()
        {
            var response = new ServiceResponse<IEnumerable<Designation>>();
            try
            {
                response.Data = await _unitOfWork.DesignationRepository.GetAll();
            }
            catch (Exception ex)
            {
                response.Message = "Designation fetching failed. :(";
                response.Success = false;
            }
            return response;
        }
    }
}
