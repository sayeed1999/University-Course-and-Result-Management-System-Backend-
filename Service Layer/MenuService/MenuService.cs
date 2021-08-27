using Data_Access_Layer;
using Entity_Layer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        private readonly RoleManager<IdentityRole> _roleManager;
        public MenuService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager) : base(dbContext)
        {
            _roleManager = roleManager;
        }

        public override async Task<ServiceResponse<Menu>> Add(Menu item)
        {
            var serviceResponse = new ServiceResponse<Menu>();
            serviceResponse.Message = "";

            Menu temp = await _dbContext.Menus.SingleOrDefaultAsync(x => x.Name == item.Name);
            if (temp != null) serviceResponse.Message += "Duplicate Menu Name found.\n";

            temp = await _dbContext.Menus.SingleOrDefaultAsync(x => x.Url == item.Url);
            if (temp != null) serviceResponse.Message += "Duplicate URL found.\n";

            if(item.ParentId != null)
            {
                temp = await _dbContext.Menus.FindAsync(item.ParentId);
                if (temp != null && temp.ParentId != null) serviceResponse.Message += "Route cannot be a child to a non-root route for this app!\n";
            }

            if(serviceResponse.Message.Length > 0)
            {
                serviceResponse.Success = false;
                return serviceResponse;
            }

            try
            {
                item.Id = 0;
                item.StatusId = Status.Inactive; // = 1
                _dbContext.Menus.Add(item);
                await _dbContext.SaveChangesAsync();
                serviceResponse.Data = item;
                serviceResponse.Message = "Item stored successfully to the database.";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = $"Item storing failed in the database\n {ex.Message}";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Menu>>> GetAllMenusByRole(string roleName)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Menu>>();
            try
            {
                var _role = await _roleManager.FindByNameAsync(roleName);
                var menuRoles = await _dbContext.MenuWiseRolePermissions
                                                .Include(x => x.Menu)
                                                .Where(x => x.RoleId == _role.Id).ToListAsync();
                
                HashSet<Menu> menus = new HashSet<Menu>();
                foreach (var item in menuRoles)
                {
                    menus.Add(item.Menu);
                }

                serviceResponse.Data = menus;
                serviceResponse.Message = "Menus fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Some error occurred while fetching data. " + ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

    public async Task<ServiceResponse<IEnumerable<Menu>>> GetAllRootMenus()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Menu>>();
            try
            {
                serviceResponse.Data = await _dbContext.Menus.Where(x => x.ParentId == null).ToListAsync();
                serviceResponse.Message = "Root menus fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Some error occurred while fetching data. " + ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
    }
}
