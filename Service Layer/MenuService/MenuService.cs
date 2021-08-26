using Data_Access_Layer;
using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.MenuService
{
    public class MenuService : Repository<Menu>, IMenuService
    {
        public MenuService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
