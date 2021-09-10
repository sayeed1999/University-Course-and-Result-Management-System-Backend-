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
            return await _unitOfWork.Designations.GetAll();
        }
    }
}
