using Entity_Layer;
using Repository_Layer;
using Repository_Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Child_Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        public Task<ServiceResponse<IEnumerable<Menu>>> GetMenusInOrder();
        public Task<ServiceResponse<IEnumerable<Menu>>> GetAllRootMenus();
        public Task<ServiceResponse<IEnumerable<Menu>>> GetAllMenusByRole(String roleName);
    }
}
