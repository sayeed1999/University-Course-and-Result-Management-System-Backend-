using Data_Access_Layer;
using Entity_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly AppSettings _appSettings;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
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
                    //var roles = model.Roles.Split(',', ' ');

                    foreach (string roleName in model.Roles)
                    {
                        string temp = roleName.Trim().ToLower();
                        if (string.IsNullOrEmpty(temp)) continue;
                        if (!(await _roleManager.RoleExistsAsync(roleName))) continue;

                        if (!(await _userManager.IsInRoleAsync(registeredUser, roleName)))
                        {
                            await _userManager.AddToRoleAsync(registeredUser, temp);
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
        [AllowAnonymous]
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
                List<string> roles = new List<string>();
                foreach (var role in dbRoles)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        roles.Add(role.Name);
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
                    //if (user.UserName != model.UserName) user.UserName = model.Email;
                    if (user.Email != model.Email) user.Email = model.Email;

                    try
                    {
                        await _userManager.UpdateAsync(user);
                    }
                    catch (Exception ex)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "User data updating not successful";
                        return BadRequest(serviceResponse);
                    }

                    try
                    {
                        foreach (string roleName in model.Roles)
                        {
                            string temp = roleName.Trim().ToLower();
                            if (string.IsNullOrEmpty(temp)) continue;
                            if (!(await _roleManager.RoleExistsAsync(roleName))) continue;
                            if (!(await _userManager.IsInRoleAsync(user, roleName)))
                            {
                                await _userManager.AddToRoleAsync(user, temp);
                            }
                        }

                        var roles2 = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles2)
                        {
                            if(model.Roles.Count(x => x == role) == 0)
                            {
                                await _userManager.RemoveFromRoleAsync(user, role);
                            }
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

            if(user == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No user found.";
                return BadRequest(serviceResponse);
            }

            List<string> roles = new List<string>();
            foreach (var role in dbRoles)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roles.Add(role.Name);
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

        [HttpPost("role/{roleName:alpha}/permission")]
        public async Task<ActionResult<ServiceResponse<MenuRole>>> RoleWiseMenuPermission(List<int> menuIds, String roleName)
        {
            var serviceResponse = new ServiceResponse<MenuRole>();

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Role not found";
                return serviceResponse;
            }

            try
            {
                // adding 
                foreach (var menuId in menuIds)
                {
                    if ((await _context.MenuWiseRolePermissions.SingleOrDefaultAsync(x => x.RoleId == role.Id && x.MenuId == menuId)) == null)
                    {
                        var newMenuRole = new MenuRole() { Id = 0, MenuId = menuId, RoleId = role.Id };
                        _context.MenuWiseRolePermissions.Add(newMenuRole);
                    }
                }
                // removing 
                var allMenus = await _context.MenuWiseRolePermissions.Where(x => x.RoleId == role.Id).ToListAsync();
                foreach(var menu in allMenus)
                {
                    if(!menuIds.Contains(menu.MenuId))
                    {
                        _context.MenuWiseRolePermissions.Remove(menu);
                    }
                }
                // all updates tracked till now.. so saving!
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            if (serviceResponse.Success == false) return BadRequest(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(Login model)
        {
            var serviceResponse = new ServiceResponse<String>(); // for the token!

            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !isValidPassword)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "The email or pasword is incorrect!";
                return BadRequest(serviceResponse);
            }

            // Email & Password correct!
            var tokenDescription = new SecurityTokenDescriptor
            { 
                Subject = new ClaimsIdentity(new Claim[]
                { 
                    new Claim("UserID", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1), //DateTime.UtcNow.AddMinutes(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret_Key)), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(securityToken);
            
            serviceResponse.Data = token;
            serviceResponse.Message = "token generated successfully!";
            return Ok(serviceResponse);
        }
    }
}
