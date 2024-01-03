using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TradeAssistant.Models;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace TradeAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthorizationController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Registration")]

        public async Task<Response> Registration([FromBody] RegisterModel registerModel)
        {

           // userId = _userManager.Users.First(x => x.UserName == _userManager.GetUserId(HttpContext.User)).Id;
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
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var isExistUser = await _userManager.FindByNameAsync(loginModel.Name); 
            if (isExistUser == null)
            {
                return   StatusCode( StatusCodes.Status401Unauthorized, new Response { Message = "Login is not corect", Status = "non" }) ;
            }

            if(!await _userManager.CheckPasswordAsync(isExistUser, loginModel.Password))
            {
               return StatusCode(StatusCodes.Status401Unauthorized, new Response { Message = "Password is not corect", Status = "non" });
            }
            

            return Ok( new Response { Message="ok", Status="ok"});
        }

    }
}
