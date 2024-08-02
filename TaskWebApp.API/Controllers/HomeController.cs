using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetContentAsync()
        {
            Thread.Sleep(5000);

            var myText = new HttpClient().GetStringAsync("https://www.google.com");

            // farklı işlemler.

            var data = await myText;// bu satıradn sonra artıkmyText eihtiyacım var.

            return Ok(data);
        }
    }
}
