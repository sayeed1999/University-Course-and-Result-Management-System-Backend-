using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.DesignationService
{
    public interface IDesignationService
    {
        public Task<ServiceResponse<IEnumerable<Designation>>> GetDesignations();
    }
}
