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

namespace Service_Layer.SemisterService
{
    public class SemisterService : ISemisterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SemisterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<Semister>>> GetSemisters()
        {
            return await _unitOfWork.Semisters.GetAll();
        }

    }
}
