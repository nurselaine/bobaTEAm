using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace ContosoCrafts.WebSite.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HelloWorldController : Controller
	{
		// 
		// GET: /HelloWorld/
		[HttpGet]
		public string Index()
		{
			return "This is my default action...";
		}
		// 
		// GET: /HelloWorld/Welcome/ 
		[HttpGet("index")]
		public string Welcome()
		{
			return "This is the Welcome action method...";
		}
	}
}