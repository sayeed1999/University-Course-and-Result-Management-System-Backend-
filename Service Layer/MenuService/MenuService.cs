using Entity_Layer;
using Repository_Layer;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.MenuService
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<Menu>> Add(Menu item)
        {
            var response = new ServiceResponse<Menu>();
            try
            {
                response = await _unitOfWork.Menus.Add(item);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Menu adding failed";
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<Menu>> DeleteById(long id)
        {
            var response = new ServiceResponse<Menu>();
            try
            {
                response = await _unitOfWork.Menus.DeleteById(id);
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
            return await _unitOfWork.Menus.GetAll();
        }

        public async Task<ServiceResponse<IEnumerable<Menu>>> GetAllMenusByRole(string roleName)
        {
            return await _unitOfWork.Menus.GetAllMenusByRole(roleName);
        }

        public async Task<ServiceResponse<IEnumerable<Menu>>> GetAllRootMenus()
        {
            return await _unitOfWork.Menus.GetAllRootMenus();
        }

        public async Task<ServiceResponse<Menu>> GetById(long id)
        {
            return await _unitOfWork.Menus.GetById(id);
        }

        public async Task<ServiceResponse<IEnumerable<Menu>>> GetMenusInOrder()
        {
            return await _unitOfWork.Menus.GetMenusInOrder();
        }

        public async Task<ServiceResponse<Menu>> Update(Menu menu)
        {
            var response = new ServiceResponse<Menu>();
            try
            {
                response = await _unitOfWork.Menus.Update(menu);
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
                response = await _unitOfWork.Menus.Update(id, menu);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Menu updating failed";
                response.Success = false;
            }
            return response;
        }
    }
}
