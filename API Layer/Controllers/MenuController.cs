﻿using Entity_Layer;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using Service_Layer.MenuService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Menu>>>> GetAllMenus()
        {
            ServiceResponse<IEnumerable<Menu>> response = await _service.GetAll();
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
    }
}