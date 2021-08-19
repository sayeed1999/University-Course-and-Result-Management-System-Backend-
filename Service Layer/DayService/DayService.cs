using Data_Access_Layer;
using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.DayService
{
    public class DayService : Repository<Day>, IDayService
    {
        public DayService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
