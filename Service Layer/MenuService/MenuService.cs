using Data_Access_Layer;
using Entity_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.MenuService
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public MenuService(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IHttpContextAccessor httpContextAccessor
        ) {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Menu>> Add(Menu item)
        {
            var serviceResponse = new ServiceResponse<Menu>();
            serviceResponse.Message = "";

            Menu temp = _unitOfWork.MenuRepository.SingleOrDefault(x => x.Name == item.Name);
            if (temp != null) serviceResponse.Message += "Duplicate Menu Name found.\n";

            temp = _unitOfWork.MenuRepository.SingleOrDefault(x => x.Url == item.Url);
            if (temp != null) serviceResponse.Message += "Duplicate URL found.\n";

            if (item.ParentId != null)
            {
                temp = await _unitOfWork.MenuRepository.Find((long)item.ParentId);
                if (temp != null && temp.ParentId != null) serviceResponse.Message += "Route cannot be a child to a non-root route for this app!\n";
            }

            if (serviceResponse.Message.Length > 0)
            {
                serviceResponse.Success = false;
                return serviceResponse;
            }

            try
            {
                item.Id = 0;
                item.StatusId = Status.Inactive; // = 1
                await _unitOfWork.MenuRepository.AddAsync(item);
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

        public async Task<ServiceResponse<Menu>> DeleteById(long id)
        {
            var response = new ServiceResponse<Menu>();
            try
            {
                _unitOfWork.MenuRepository.DeleteById(id);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Menu deleting failed";
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<Menu>>> GetAll()
        {
            var response = new ServiceResponse<IEnumerable<Menu>>();
            try
            {
                response.Data = await _unitOfWork.MenuRepository.GetAll();
            }
            catch (Exception ex)
            {
                response.Message = "Menu fetching failed. :(";
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<Menu>>> GetAllMenusByRole(string roleName)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Menu>>();
            try
            {
                var _role = await _roleManager.FindByNameAsync(roleName);
                var menuRoles = _unitOfWork.MenuWiseRolePermissionRepository
                                                 .Where(x => x.RoleId == _role.Id, x => x.Menu)
                                                 .ToList();

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
                serviceResponse.Data = _unitOfWork.MenuRepository.Where(x => x.ParentId == null).ToList();
                serviceResponse.Message = "Root menus fetched successfully from the database";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Some error occurred while fetching data. " + ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Menu>> GetById(long id)
        {
            var response = new ServiceResponse<Menu>();
            response.Data = await _unitOfWork.MenuRepository.Find(id);
            if(response.Data == null)
            {
                response.Message = "Not found";
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<Menu>>> GetMenusInOrder()
        {
            var response = new ServiceResponse<IEnumerable<Menu>>();
            var listOfMenus = new List<Menu>();
            response.Data = listOfMenus;

            string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value;
            if (String.IsNullOrEmpty(userId))
            {
                return response;
            }
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            var roleNames = await _userManager.GetRolesAsync(user);

            var availableMenuIds = new HashSet<long>();
            foreach (var role in roleNames)
            {
                var temp = _unitOfWork.MenuWiseRolePermissionRepository.Where(x => x.Role.Name == role).ToList();
                foreach (var menurole in temp)
                {
                    availableMenuIds.Add(menurole.MenuId);
                }
            }

            var menus = _unitOfWork.MenuRepository.Where(x => x.ParentId == null, x => x.ChildMenus).ToList();
            foreach (var menu in menus)
            {
                var temp = new List<Menu>();
                foreach (var child in menu.ChildMenus)
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

            return response;
        }

        public async Task<ServiceResponse<Menu>> Update(Menu menu)
        {
            var response = new ServiceResponse<Menu>();
            try
            {
                _unitOfWork.MenuRepository.Update(menu);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Menu updating failed";
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<Menu>> Update(long id, Menu menu)
        {
            var response = new ServiceResponse<Menu>();
            try
            {
                _unitOfWork.MenuRepository.Update(id, menu);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Menu updating failed";
                response.Success = false;
            }
            return response;
        }

        /// Role Related Operations ..
        
        public async Task AddMenusToRoleAsync(List<long> menuIds, string roleId)
        {
            foreach (var menuId in menuIds)
            {
                if (_unitOfWork.MenuWiseRolePermissionRepository.SingleOrDefault(x => x.RoleId == roleId && x.MenuId == menuId) == null)
                {
                    var newMenuRole = new MenuRole() { Id = 0, MenuId = menuId, RoleId = roleId };
                    await _unitOfWork.MenuWiseRolePermissionRepository.AddAsync(newMenuRole);
                }
            }
        }

        public void RemoveMenusFromRole(List<long> menuIds, string roleId)
        {
            IEnumerable<MenuRole> allMenus = _unitOfWork.MenuWiseRolePermissionRepository.Where(x => x.RoleId == roleId).ToList();
            foreach (var menu in allMenus)
            {
                if (!menuIds.Contains(menu.MenuId))
                {
                    _unitOfWork.MenuWiseRolePermissionRepository.Delete(menu);
                }
            }
        }

        public async Task UpdateMenuPermissionByRole(List<long> menuIds, string roleId)
        {
            try
            {
                await AddMenusToRoleAsync(menuIds, roleId);
                RemoveMenusFromRole(menuIds, roleId);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Menu permission for role not updated. :(");
            }
        }
    }
}
