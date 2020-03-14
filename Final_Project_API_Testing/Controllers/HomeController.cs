using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Final_Project_API_Testing.Models;
using System.Net.Http;

namespace Final_Project_API_Testing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://opentdb.com/api.php");

            var response = await client.GetAsync($"?amount=1&type=multiple");
            var giphy = await response.Content.ReadAsAsync<Question>();
            return View(giphy);
        }

        public async Task<IActionResult> Result(string correct, string incorrect)
        {
            string outcome = "";

            //success!
            if (correct != null)
            {
                outcome = "Success!";
            }
            else
            {
                outcome = "Wrong!";
            }
            return View("Result", outcome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
