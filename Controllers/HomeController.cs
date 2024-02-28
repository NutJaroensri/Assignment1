using GBC_Travel_Group83.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GBC_Travel_Group83.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GeneralSearch(string searchType, string searchString)
        {
            if (searchType == "Bookings")
            {
                // Redirect to Project search
                return RedirectToAction("Search", "Bookings", new { searchString });
            }
            else if (searchType == "Flights")
            {
                //Redirect to Flights search
                int defaultBookingId = 1;
                return RedirectToAction("Search", "Flights", new { bookingId = defaultBookingId, searchString });
            }
            else if (searchType == "Flights")
            {
                //Redirect to RentalCars search
                int defaultBookingId = 1;
                return RedirectToAction("Search", "RentalCars", new { bookingId = defaultBookingId, searchString });
            }

            else if (searchType == "Hotels")
            {
                //Redirect to RentalCars search
                int defaultBookingId = 1;
                return RedirectToAction("Search", "Hotels", new { bookingId = defaultBookingId, searchString });
            }


            return RedirectToAction("Index", "Home");

        }

        public IActionResult NotFound(int statusCode)
        {
            _logger.LogInformation("Not Found action called with status code: {StatusCode}", statusCode);

            if (statusCode == 404)
            {
                return View("NotFound");
            }

            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
