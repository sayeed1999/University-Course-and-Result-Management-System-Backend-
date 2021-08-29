using Data_Access_Layer;
using Entity_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.MenuService
{
    public class MenuService : Repository<Menu>, IMenuService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MenuService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<ServiceResponse<IEnumerable<Menu>>> GetMenusInOrder()
        {
            string userId = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "UserID").Value;
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            var roleNames = await _userManager.GetRolesAsync(user);

            var availableMenuIds = new HashSet<int>();
            foreach(var role in roleNames)
            {
                var temp = await _dbContext.MenuWiseRolePermissions.Where(x => x.Role.Name == role).ToListAsync();
                foreach(var menurole in temp)
                {
                    availableMenuIds.Add(menurole.MenuId);
                }
            }


            var response = new ServiceResponse<IEnumerable<Menu>>();
            var listOfMenus = new List<Menu>();

            var menus = await _dbContext.Menus.Include(x => x.ChildMenus).Where(x => x.ParentId == null).ToListAsync();
            foreach(var menu in menus)
            {
                var temp = new List<Menu>();
                foreach(var child in menu.ChildMenus)
                {
                    if (availableMenuIds.Contains(child.Id))
                    {
                        temp.Add(child);
                    }
                }
                var menuInList = menu;
                menuInList.ChildMenus = temp;
                listOfMenus.Add(menuInList);
            }

            response.Data = listOfMenus;
            return response;
        }
    }
}
