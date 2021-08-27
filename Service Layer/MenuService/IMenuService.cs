using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.MenuService
{
    public interface IMenuService : IRepository<Menu>
    {
        public Task<ServiceResponse<IEnumerable<Menu>>> GetAllRootMenus();
        public Task<ServiceResponse<IEnumerable<Menu>>> GetAllMenusByRole(String roleName);
    }
}
