using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Repository_Layer.Child_Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _service;

        public MenuController(IMenuRepository service)
        {
            _service = service;
        }

        [AllowAnonymous] // responsible for displaying menus in the frontend
        [HttpGet("InOrder")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Menu>>>> GetMenusInOrder()
        {
            ServiceResponse<IEnumerable<Menu>> response = await _service.GetMenusInOrder();
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Menu>>>> GetAllMenus()
        {
            ServiceResponse<IEnumerable<Menu>> response = await _service.GetAll();
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpGet]
        [Route("Root")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Menu>>>> GetAllRootMenus()
        {
            ServiceResponse<IEnumerable<Menu>> response = await _service.GetAllRootMenus();
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Menu>>> GetMenuById(int id)
        {
            ServiceResponse<Menu> response = await _service.GetById(id);
            if (response.Success) return Ok(response);
            return BadRequest(response);
}

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Menu>>> CreateMenu(Menu menu)
        {
            ServiceResponse<Menu> response = await _service.Add(menu);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Menu>>> UpdateMenu([FromBody] Menu menu)
        {
            ServiceResponse<Menu> response = await _service.Update(menu);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Menu>>> UpdateMenu([FromBody]Menu menu, [FromRoute]int id)
        {
            ServiceResponse<Menu> response = await _service.Update(id, menu);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Menu>>> DeleteMenuById(int id)
        {
            ServiceResponse<Menu> response = await _service.DeleteById(id);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("{roleName}")] // alpha stands for string here
        public async Task<ActionResult<ServiceResponse<IEnumerable<Menu>>>> GetAllMenusByRole(string roleName)
        {
            ServiceResponse<IEnumerable<Menu>> response = await _service.GetAllMenusByRole(roleName);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
    }
}
