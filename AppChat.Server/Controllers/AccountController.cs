using AppChat.Domain;
using AppChat.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AppChat.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountManager _accountManager;

        public AccountController(ILogger<AccountController> logger, IAccountManager userManager)
        {
            _logger = logger;
            _accountManager = userManager;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        /// <summary>
        /// TODO: implement validations, I deliberated assume the risks in not implement validations once it is for a pratical test
        /// then here I believe we will accept the happy path
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Account account)
        {
            await _accountManager.CreateAsync(account);
            return Ok();
        }

        //public IActionResult SignIn([FromBody])
    }
}