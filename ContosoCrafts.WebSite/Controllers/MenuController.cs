using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using static ContosoCrafts.WebSite.Controllers.ProductsController;

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

		[HttpPatch]
		public ActionResult Patch([FromBody] RatingRequest request)
		{
			if (request?.ProductId == null)
				return BadRequest();

			MenuServices.AddRating(request.ProductId, request.Rating);

			return Ok();
		}

	}
}
