using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using System.Text;
using TradeAssistant.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace TradeAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthorizationController(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                _userManager = userManager;
                _configuration = configuration;
                _roleManager = roleManager;

            }
            catch (Exception ex)
            {

                
            }
            
        }

        [HttpPost]
        [Route("Registration")]

        public async Task<Response> Registration([FromBody] RegisterModel registerModel)
        {

            // userId = _userManager.Users.First(x => x.UserName == _userManager.GetUserId(HttpContext.User)).Id;
            try
            {
                var isExistUser = await _userManager.FindByNameAsync(registerModel.Name);
                if (isExistUser != null)
                {
                    return new Response { Message = "User is exist", Status = "not" };
                }
                var appUser = new ApplicationUser();
                appUser.Email = registerModel.Email;
                appUser.UserName = registerModel.Name;
                appUser.FirstName = registerModel.FirstName;
                appUser.LastName = registerModel.LastName;
                appUser.SecurityStamp = Guid.NewGuid().ToString();
                var res = await _userManager.CreateAsync(appUser, registerModel.Password);

                if (!res.Succeeded)
                {
                    return new Response { Message = "Db server problem", Status = "not" };
                }

                return new Response { Message = "User register", Status = "ok" };


            }
            catch (Exception ex)
            {

                return new Response { Message=ex.Message,Status="error"};
            }
           
        }
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var isExistUser = await _userManager.FindByNameAsync(loginModel.Name);
                if (isExistUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new Response { Message = "Login is not corect", Status = "non" });
                }

                if (!await _userManager.CheckPasswordAsync(isExistUser, loginModel.Password))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new Response { Message = "Password is not corect", Status = "non" });
                }

                var userRoles = await _userManager.GetRolesAsync(isExistUser);
                var authClaim = new List<Claim> { new Claim(ClaimTypes.NameIdentifier,isExistUser.UserName) };
                foreach (var role in userRoles)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, role));

                }
                var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var time=DateTime.Now.AddHours(6);
                var token = new JwtSecurityToken
                (

                    issuer: _configuration["JWT:ValidIssuier"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: time,
                    claims:authClaim,
                    signingCredentials:new SigningCredentials(authSignInKey,SecurityAlgorithms.HmacSha256)
                ) ;
                return Ok(new {token=new JwtSecurityTokenHandler().WriteToken(token),expiration=token.ValidTo} );

            }
            catch (Exception ex )
            {

                return Ok(new Response { Message = ex.Message, Status = "error" });
            }
        }

    }
}
