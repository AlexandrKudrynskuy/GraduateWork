using BLL.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TradeAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService _testService;

        public TestController(TestService testService)
        {
            _testService = testService;
            
        }
        [HttpPost]
        public  async Task<IActionResult> PostTest()
        {
            var test = new Test() { Id = 1, Name = "Sasha", Age = 18, PhotoPath = "Path" };
            var res= await  _testService.CreateAsync(test);
            return Ok(res);
        }
    }
}
