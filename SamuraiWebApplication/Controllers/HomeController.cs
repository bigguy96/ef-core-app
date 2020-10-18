using System;
using System.Threading.Tasks;
using EfCoreApp.Domain;
using EfCoreApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SamuraiWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISamuraiServices _samuraiServices;

        public HomeController(ILogger<HomeController> logger, ISamuraiServices samuraiServices)
        {
            _logger = logger;
            _samuraiServices = samuraiServices;
        }

        public async Task<IActionResult> Index()
        {
            var guid = Guid.NewGuid().ToString().Substring(0, 6);
            var samurai = new Samurai
            {
                Name = guid,
                Clan = new Clan { ClanName = guid },
                Horse = new Horse { Name = guid }
            };
            await _samuraiServices.Add(samurai);
            var samurais = await _samuraiServices.GetAll();

            return View();
        }
    }
}