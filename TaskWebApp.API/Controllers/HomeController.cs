using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetContentAsync(CancellationToken cancellationToken)
        {
            #region Before used

            //Thread.Sleep(5000);

            //var myText = new HttpClient().GetStringAsync("https://www.google.com");

            //// farklı işlemler.

            //var data = await myText;// bu satıradn sonra artıkmyText eihtiyacım var.

            //return Ok(data);
            #endregion

            #region CancellationToken
            try
            {
                _logger.LogInformation("İstek başladı.");

                await Task.Delay(5000, cancellationToken);

                var myText = new HttpClient().GetStringAsync("https://www.google.com");

                // farklı işlemler.

                var data = await myText;// bu satıradn sonra artıkmyText eihtiyacım var.
                _logger.LogInformation("İstek bitti.");
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogInformation("istek iptal edildi." + e.Message);
                return BadRequest();
            }
            #endregion

        }
    }
}
