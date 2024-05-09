using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MenuController : ControllerBase
    {

        private readonly ILogger<MenuController> _logger; 

        public JsonFileMenuServices MenuServices { get;  }

        public MenuController(JsonFileMenuServices menuServices, ILogger<MenuController> logger)
        {
			MenuServices = menuServices;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<MenuItem> Get() => MenuServices.GetMenuItems();
    }
}
