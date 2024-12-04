using Microsoft.AspNetCore.Mvc;
using My_Trip_Log.Models;

namespace My_Trip_Log.Controllers
{
    public class TripsController : Controller
    {
        private readonly AppDbContext _context;

        public TripsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Step1()
        {
            TempData["SubHeader"] = "Enter Trip Destination and Dates";
            return View();
        }

        [HttpPost]
        public IActionResult Step1(Trip trip)
        {
            if (ModelState.IsValid)
            {
                TempData["Destination"] = trip.Destination;
                TempData["StartDate"] = trip.StartDate.ToString();
                TempData["EndDate"] = trip.EndDate.ToString();

                return RedirectToAction(nameof(Step2));
            }

            TempData["SubHeader"] = "Enter Trip Destination and Dates";
            return View(trip);
        }

        [HttpGet]
        public IActionResult Step2()
        {
            TempData["SubHeader"] = $"Add Accommodations for: {TempData["Destination"]}";
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public IActionResult Step2(string? accommodations, string? phone, string? email)
        {
            TempData["Accommodations"] = accommodations;
            TempData["Phone"] = phone;
            TempData["Email"] = email;

            TempData.Keep();
            return RedirectToAction(nameof(Step3));
        }

        [HttpGet]
        public IActionResult Step3()
        {
            TempData["SubHeader"] = $"Add Activities for: {TempData["Destination"]}";
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public IActionResult Step3(string? activities)
        {
            TempData["Activities"] = activities;

            // Save trip to database
            if (TempData.TryGetValue("StartDate", out var startDateStr) &&
                DateTime.TryParse(startDateStr.ToString(), out var startDate) &&
                TempData.TryGetValue("EndDate", out var endDateStr) &&
                DateTime.TryParse(endDateStr.ToString(), out var endDate))
            {
                var trip = new Trip
                {
                    Destination = TempData["Destination"] as string,
                    StartDate = startDate,
                    EndDate = endDate,
                    Accommodations = TempData["Accommodations"] as string,
                    Phone = TempData["Phone"] as string,
                    Email = TempData["Email"] as string,
                    Activities = TempData["Activities"] as string
                };

                _context.Trips.Add(trip);
                _context.SaveChanges(); // Save the trip to the database
            }

            TempData.Clear();
            TempData["Message"] = "Trip added successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var trips = _context.Trips.ToList();
            TempData["SubHeader"] = null;
            return View(trips);
        }

        public IActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var trip = _context.Trips.FirstOrDefault(t => t.TripID == id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost]
        public IActionResult Edit(Trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Trips.Update(trip);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var trip = _context.Trips.FirstOrDefault(t => t.TripID == id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var trip = _context.Trips.FirstOrDefault(t => t.TripID == id);

            if (trip != null)
            {
                _context.Trips.Remove(trip);
                _context.SaveChanges();
                TempData["Message"] = "Trip successfully deleted.";
            }
            else
            {
                TempData["Message"] = "Trip not found or already deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}

