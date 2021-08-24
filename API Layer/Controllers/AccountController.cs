using Data_Access_Layer;
using Entity_Layer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            //_context = context;
        }
        
        [HttpPost("Register")]
        public async Task<ServiceResponse<RegisterDto>> Register(RegisterDto model)
        {
            var serviceResponse = new ServiceResponse<RegisterDto>();
            serviceResponse.Data = model;
            serviceResponse.Data.Roles = new HashSet<string>();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.FirstName + model.LastName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                // both password & confirmPassword  = "AAAA12";
                var result = await _userManager.CreateAsync(user, "AAAA12");

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    serviceResponse.Message = "User registered successfully.";
                    ApplicationUser registeredUser = await _userManager.FindByEmailAsync(user.Email);

                    //await _userManager.AddToRolesAsync(registeredUser, model.Roles);

                    foreach(string roleName in model.Roles)
                    {
                        string temp = roleName.Trim().ToLower();
                        if (string.IsNullOrEmpty(temp)) continue;
                        if (await _roleManager.RoleExistsAsync(roleName) == false) continue;

                        if(await _userManager.IsInRoleAsync(registeredUser, roleName))
                        {
                            await _userManager.AddToRoleAsync(registeredUser, temp);
                            serviceResponse.Data.Roles.Add(temp);
                        }
                        serviceResponse.Message += " And user is added to the returned roles.";
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = result.Errors.ToList().ToString();
                }
            }
            else
            {
                serviceResponse.Message = "Model State is not valid.";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        [HttpPost("Roles")]
        public async Task<ServiceResponse<RoleDto>> CreateRoles([FromBody] RoleDto newRole)
        {
            var serviceResponse = new ServiceResponse<RoleDto>();
            serviceResponse.Data = newRole;
            try
            {
                //_context.Roles.Add(new IdentityRole() { Name = newRole.Name });
                //await _context.SaveChangesAsync();

                await _roleManager.CreateAsync(new IdentityRole() { Name = newRole.Name });
                serviceResponse.Message = "New role created!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
