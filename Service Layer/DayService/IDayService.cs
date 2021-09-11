using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.DayService
{
    public interface IDayService
    {
        public Task<ServiceResponse<IEnumerable<Day>>> GetDays(); // Story 08
    }
}
