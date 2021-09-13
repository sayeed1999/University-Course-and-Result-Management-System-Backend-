using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.MenuService
{
    public interface IMenuService
    {
        public Task<ServiceResponse<Menu>> Add(Menu item);
        public Task<ServiceResponse<IEnumerable<Menu>>> GetAllMenusByRole(string roleName);
        public Task<ServiceResponse<IEnumerable<Menu>>> GetAllRootMenus();
        public Task<ServiceResponse<IEnumerable<Menu>>> GetMenusInOrder();
        public Task<ServiceResponse<IEnumerable<Menu>>> GetAll();
        public Task<ServiceResponse<Menu>> GetById(long id);
        public Task<ServiceResponse<Menu>> Update(Menu menu);
        public Task<ServiceResponse<Menu>> Update(long id, Menu menu);
        public Task<ServiceResponse<Menu>> DeleteById(long id);
    }
}
