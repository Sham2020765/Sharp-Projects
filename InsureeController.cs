using System;
using System.Web.Mvc;
using YourProjectName.Models;

namespace YourProjectName.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,FullCoverage,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                // a. Start with a base of $50 / month
                decimal monthlyTotal = 50m;

                // Calculate age based on DateOfBirth
                int age = DateTime.Today.Year - insuree.DateOfBirth.Year;
                if (insuree.DateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;

                // b. If the user is 18 or under, add $100
                if (age <= 18)
                {
                    monthlyTotal += 100;
                }
                // c. If the user is from 19 to 25, add $50
                else if (age >= 19 && age <= 25)
                {
                    monthlyTotal += 50;
                }
                // d. If the user is 26 or older, add $25
                else if (age >= 26)
                {
                    monthlyTotal += 25;
                }

                // e. If the car's year is before 2000, add $25
                if (insuree.CarYear < 2000)
                {
                    monthlyTotal += 25;
                }
                // f. If the car's year is after 2015, add $25
                else if (insuree.CarYear > 2015)
                {
                    monthlyTotal += 25;
                }

                // g. If the car's Make is a Porsche, add $25
                if (!string.IsNullOrEmpty(insuree.CarMake) && insuree.CarMake.ToLower() == "porsche")
                {
                    monthlyTotal += 25;

                    // h. If Make is Porsche and Model is a Carrera, add an additional $25
                    if (!string.IsNullOrEmpty(insuree.CarModel) && insuree.CarModel.ToLower().Contains("carrera"))
                    {
                        monthlyTotal += 25;
                    }
                }

                // i. Add $10 for every speeding ticket
                monthlyTotal += (insuree.SpeedingTickets * 10);

                // j. If the user has ever had a DUI, add 25% to the total
                if (insuree.DUI)
                {
                    monthlyTotal += (monthlyTotal * 0.25m);
                }

                // k. If it's full coverage, add 50% to the total
                if (insuree.FullCoverage) 
                {
                    monthlyTotal += (monthlyTotal * 0.50m);
                }

                // Assign the calculated total to the Quote property
                insuree.Quote = monthlyTotal;

                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }
    }
}
