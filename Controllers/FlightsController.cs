using GBC_Travel_Group83.Data;
using GBC_Travel_Group83.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group83.Controllers
{
    [Route("Flights")]
    public class FlightsController : Controller
    {

        private readonly AppDbContext _db;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(AppDbContext db, ILogger<FlightsController> logger) { 
            _db = db;
            _logger = logger;
        }
        [HttpGet("Index/{bookingId:int}")]
        public IActionResult Index(int bookingId)
        {
            var flights = _db.Flights.Where(t => t.BookingId == bookingId).ToList();
            ViewBag.BookingId = bookingId;
            return View(flights);
        }

		[HttpGet("Details/{id:int}")]
		public IActionResult Details(int id)
		{
            var flight = _db.Flights.Include(f => f.Booking).FirstOrDefault(f => f.BookingId == id);

			if (flight == null)
			{
				return NotFound();
			}
			return View(flight);


		}

        [HttpGet("Create/{bookingId:int}")]
        public IActionResult Create(int bookingId)
        {
            var booking = _db.Bookings.Find(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            var flight = new Flight
            {
                BookingId = bookingId,
            };
            return View(flight);


        }

        [HttpPost("Create/{bookingId:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FlightNumber", "Airline", "Origin", "Destination", "DepartureTime", "ArrivalTime", "FlightDurationMin", "Price", "BookingId")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _db.Flights.Add(flight);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { bookingId = flight.BookingId });
            }

            ViewBag.Projects = new SelectList(_db.Bookings, "BookingId", "Name", flight.BookingId);
            return View(flight);
        }
    }

    
}
