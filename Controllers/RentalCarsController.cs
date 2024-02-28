using GBC_Travel_Group83.Data;
using GBC_Travel_Group83.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group83.Controllers
{
    [Route("RentalCars")]
    public class RentalCarsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<RentalCarsController> _logger;

        public RentalCarsController(AppDbContext db, ILogger<RentalCarsController> logger)
        {
            _db = db;
            _logger = logger;
        }
       

        [HttpGet("")]
        public IActionResult Index()
        {
            return View(_db.RentalCars.ToList());
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var rentalCar = _db.RentalCars.FirstOrDefault(r => r.RentalCarID == id);
            if (rentalCar == null)
            {
                return NotFound();
            }
            return View(rentalCar);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RentalCar rentalCar)
        {
            if (ModelState.IsValid)
            {
                _db.RentalCars.Add(rentalCar);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(rentalCar);
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var rentalCar = _db.RentalCars.Find(id);
            if (rentalCar == null)
            {
                return NotFound();
            }
            return View(rentalCar);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RentalCarID, PlateNumber, RentalCompany, ModelName, ModelDescription, ModelType, Price, IsAvailable, BookingId")] RentalCar rentalCar)
        {
           
            if (id != rentalCar.RentalCarID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(rentalCar);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalCarExists(rentalCar.RentalCarID))
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
            return View(rentalCar);
        }

        private bool RentalCarExists(int id)
        {
            return _db.RentalCars.Any(e => e.RentalCarID == id);
        }

        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var rentalCar = _db.RentalCars.FirstOrDefault(r => r.RentalCarID == id);
            if (rentalCar == null)
            {
                return NotFound();
            }
            return View(rentalCar);
        }

        [HttpPost("DeleteConfirmed/{id:int}")]
        [HttpPost, ActionName("DeleteConfirm")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int rentalCarID)
        {
            var rentalCar = _db.RentalCars.Find(rentalCarID);
            if (rentalCar != null)
            {
                _db.RentalCars.Remove(rentalCar);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }

        [HttpGet("Search/{searchString?}")]
        public async Task<IActionResult> Search(string searchString)
        {
            var rentalCarsQuery = from r in _db.RentalCars
                                select r;
            bool searchPerformed = !String.IsNullOrEmpty(searchString);
            if (searchPerformed)
            {
                rentalCarsQuery = rentalCarsQuery.Where(r => r.ModelName.Contains(searchString)
                                                             || r.RentalCompany.Contains(searchString));
            }

            var rentalCars = await rentalCarsQuery.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;
            return View("Index", rentalCars);

        }

    }
}


   

       

        

       

        

       

      

        



     