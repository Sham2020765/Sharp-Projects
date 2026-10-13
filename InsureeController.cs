using System;
using System.Linq;
using System.Web.Mvc;

namespace CarInsuranceProject.Controllers
{
    public class InsureeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // 1. Admin Action Method requested by the instructor
        public ActionResult Admin()
        {
            var dbQuotes = db.Insurees.ToList(); 
            return View(dbQuotes);
        }

        // Index Action Method
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // Create GET Action Method
        public ActionResult Create()
        {
            return View();
        }

        // 2. Create POST Action Method with automatic Quote calculation logic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                // Base quote is $50
                decimal baseQuote = 50;

                // Calculate exact age
                int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
                if (insuree.DateOfBirth.AddYears(age) > DateTime.Now) age--;

                // Age logic rules
                if (age <= 18) baseQuote += 100;
                else if (age >= 19 && age <= 25) baseQuote += 50;
                else if (age >= 26) baseQuote += 25;

                // Car year logic rules
                if (insuree.CarYear < 2000) baseQuote += 25;
                if (insuree.CarYear > 2015) baseQuote += 25;

                // Car make and model logic rules (Porsche / Carrera)
                if (insuree.CarMake != null && insuree.CarMake.ToLower() == "porsche")
                {
                    baseQuote += 25;
                    if (insuree.CarModel != null && insuree.CarModel.ToLower().Contains("carrera"))
                    {
                        baseQuote += 50;
                    }
                }

                // Speeding tickets rule ($10 per ticket)
                baseQuote += (insuree.SpeedingTickets * 10);

                // DUI rule (add 25%)
                if (insuree.DUI)
                {
                    baseQuote = baseQuote * 1.25m;
                }

                // Full coverage rule (add 50%)
                if (insuree.CoverageType != null && insuree.CoverageType.ToLower() == "full")
                {
                    baseQuote = baseQuote * 1.50m;
                }

                // Assign the final calculated value to the Quote field
                insuree.Quote = baseQuote;

                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }
    }
}
