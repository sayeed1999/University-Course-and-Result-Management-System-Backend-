using Entity_Layer;
using Repository_Layer;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.DayService
{
    public class DayService : IDayService
    {
        private readonly IUnitOfWork unitOfWork;

        public DayService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<Day>>> GetDays()
        {
            return await unitOfWork.Days.GetAll();
        }
    }
}
