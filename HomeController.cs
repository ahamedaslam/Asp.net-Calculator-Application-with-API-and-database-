using Calc_ui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Calc_ui.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Calc c)
        {
            int a = c.Value1;
            int b = c.Value2;
            int result;

            if (c.Calculate == "add")
            {
                result = await CallApiAdd(a, b);
            }
            else if (c.Calculate == "sub")
            {
                result = await CallApiSub(a, b);
            }
            else if (c.Calculate == "mul")
            {
                result = await CallApiMul(a, b);
            }
            else
            {
                result = await CallApiDiv(a, b);
            }

            c.Total = result;
            ViewData["Total"] = c.Total;

            return View();
        }

        private async Task<int> CallApiAdd(int value1, int value2)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7086/api/Calculator/addition/{value1}/{value2}");
                var content = await response.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(content);
                return resultObject.Value<int>("result");
            }
        }

        private async Task<int> CallApiSub(int value1, int value2)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7086/api/Calculator/subtraction/{value1}/{value2}");
                var content = await response.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(content);
                return resultObject.Value<int>("result1");
            }
        }

        private async Task<int> CallApiMul(int value1, int value2)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7086/api/Calculator/multiplication/{value1}/{value2}");
                var content = await response.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(content);
                return resultObject.Value<int>("result2");
            }
        }

        private async Task<int> CallApiDiv(int value1, int value2)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7086/api/Calculator/division/{value1}/{value2}");
                var content = await response.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(content);
                return resultObject.Value<int>("result3");
            }
        }
    }
}
