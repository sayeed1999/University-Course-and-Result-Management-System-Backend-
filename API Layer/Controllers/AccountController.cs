using Data_Access_Layer;
using Entity_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<RegisterDto>>> Register(RegisterDto model)
        {
            var serviceResponse = new ServiceResponse<RegisterDto>();
            serviceResponse.Data = model;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
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
                    var roles = model.Roles.Split(',', ' ');

                    foreach (string roleName in roles)
                    {
                        string temp = roleName.Trim().ToLower();
                        if (string.IsNullOrEmpty(temp)) continue;
                        if (!(await _roleManager.RoleExistsAsync(roleName))) continue;

                        if (!(await _userManager.IsInRoleAsync(registeredUser, roleName)))
                        {
                            await _userManager.AddToRoleAsync(registeredUser, temp);
                            serviceResponse.Data.Roles += roleName + ", ";
                        }
                    }
                    serviceResponse.Message += " User is added to the respective roles.";

                }
                else
                {
                    serviceResponse.Message = "Errors occured:-\n";
                    foreach (var error in result.Errors)
                    {
                        serviceResponse.Message += error.Description + "\n";
                    }
                    serviceResponse.Success = false;
                }
            }
            else
            {
                serviceResponse.Message = "Model State is not valid. Send proper data";
                serviceResponse.Success = false;
            }
            if (serviceResponse.Success) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpPost("Roles")]
        public async Task<ActionResult<ServiceResponse<RoleDto>>> CreateRoles([FromBody] RoleDto newRole)
        {
            var serviceResponse = new ServiceResponse<RoleDto>();
            serviceResponse.Data = newRole;
            try
            {
                //_context.Roles.Add(new IdentityRole() { Name = newRole.Name });
                //await _context.SaveChangesAsync();

                await _roleManager.CreateAsync(new IdentityRole() { Name = newRole.Name.Trim().ToLower() });
                serviceResponse.Message = "New role created!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            if (serviceResponse.Success) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<string>>>> GetAllRoles()
        {
            ServiceResponse<IEnumerable<string>> serviceResponse = new ServiceResponse<IEnumerable<string>>();
            var temp = new List<string>();
            try
            {
                foreach (var role in _context.Roles)
                {
                    temp.Add(role.Name);
                }
                if (temp.Count == 0) serviceResponse.Message = "No roles found. Try inserting roles.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            serviceResponse.Data = temp;
            if (serviceResponse.Success) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpGet("AllUsers")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<RegisterDto>>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<RegisterDto>>();
            var users = new List<RegisterDto>();
            List<IdentityRole> dbRoles = await _roleManager.Roles.ToListAsync();

            foreach (var user in _userManager.Users)
            {
                String roles = "";
                foreach (var role in dbRoles)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        if (!string.IsNullOrEmpty(roles)) roles += ",";
                        roles += role.Name;
                    }
                }

                users.Add(
                    new RegisterDto()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Roles = roles,
                        UserName = user.UserName
                    }
                );
            }
            serviceResponse.Data = users;
            return Ok(serviceResponse);
        }

        [HttpPut("{email}/Update")]
        public async Task<ActionResult<ServiceResponse<RegisterDto>>> Update(RegisterDto model, [FromRoute] string email)
        {
            var serviceResponse = new ServiceResponse<RegisterDto>();
            serviceResponse.Data = model;

            if(email != model.Email)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Email in the route and email in the form body don't match";
                return serviceResponse;
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Model invalid. No user found";
                }
                else
                {
                    if (user.FirstName != model.FirstName) user.FirstName = model.FirstName;
                    if (user.LastName != model.LastName) user.LastName = model.LastName;
                    if (user.UserName != model.UserName) user.UserName = model.UserName;
                    if (user.Email != model.Email) user.Email = model.Email;

                    try
                    {
                        await _userManager.UpdateAsync(user);
                    }
                    catch (Exception ex)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "User data updating not successful";
                    }

                    try
                    {
                        var roles = model.Roles.Split(',', ' ');
                        foreach (string roleName in roles)
                        {
                            string temp = roleName.Trim().ToLower();
                            if (string.IsNullOrEmpty(temp)) continue;
                            if (!(await _roleManager.RoleExistsAsync(roleName))) continue;
                            if (!(await _userManager.IsInRoleAsync(user, roleName)))
                            {
                                await _userManager.AddToRoleAsync(user, temp);
                                serviceResponse.Data.Roles += roleName + ", ";
                            }
                        }

                        roles = (string[])await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            await _userManager.RemoveFromRoleAsync(user, role);
                        }
                    }
                    catch (Exception ex)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "User updated without the roles for some error";
                    }
                }
            }
            else
            {
                serviceResponse.Message = "Model you provided is not valid.";
                serviceResponse.Success = false;
            }

            if (serviceResponse.Success) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<ServiceResponse<RegisterDto>>> GetUserByEmail([FromRoute] string email)
        {
            var serviceResponse = new ServiceResponse<RegisterDto>();

            List<IdentityRole> dbRoles = await _roleManager.Roles.ToListAsync();

            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            String roles = "";
            foreach (var role in dbRoles)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    if (!string.IsNullOrEmpty(roles)) roles += ",";
                    roles += role.Name;
                }
            }

            RegisterDto ret = new RegisterDto() {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = roles,
                UserName = user.UserName
            };

            serviceResponse.Data = ret;
            return Ok(serviceResponse);
        }
    }
}
