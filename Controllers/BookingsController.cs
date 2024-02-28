using GBC_Travel_Group83.Data;
using GBC_Travel_Group83.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GBC_Travel_Group83.Controllers
{
    [Route("Bookings")]
    public class BookingsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(AppDbContext db, ILogger<BookingsController> logger) {
            _db = db;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View(_db.Bookings.ToList());
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var booking = _db.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _db.Bookings.Add(booking);
                _db.SaveChanges();
              
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var booking = _db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookingId, Name, Description, Date, TotalPrice")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(booking);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();

                    }
                    else
                    {
                        throw;
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        private bool BookingExists(int id)
            {
                return _db.Bookings.Any(e => e.BookingId == id);
            }

        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var booking = _db.Bookings.FirstOrDefault(p => p.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost("DeleteConfirmed/{id:int}")]
        [HttpPost, ActionName("DeleteConfirm")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int BookingId)
        {
            var booking = _db.Bookings.Find(BookingId);
            if (booking != null)
            {
                _db.Bookings.Remove(booking);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }



        [HttpGet("Search/{searchString?}")]
            public async Task<IActionResult> Search(string searchString)
            {
                var bookingsQuery = from b in _db.Bookings
                                    select b;
                bool searchPerformed = !String.IsNullOrEmpty(searchString);
                if (searchPerformed)
                {
                    bookingsQuery = bookingsQuery.Where(b => b.Name.Contains(searchString)
                                                                 || b.Description.Contains(searchString));
                }

                var bookings = await bookingsQuery.ToListAsync();
                ViewData["SearchPerformed"] = searchPerformed;
                ViewData["SearchString"] = searchString;
                return View("Index", bookings);

            }
        }
}
