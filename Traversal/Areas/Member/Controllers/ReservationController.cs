using BusinessLayer.Concrete;
using BusinessLayer.Validations;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Security.Claims;

namespace Traversal.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/Reservation/[action]")]
    public class ReservationController : Controller
    {
        DestinationManager _destinationManager = new DestinationManager(new EfDestinationDal());
        ReservationManager _reservationManager = new ReservationManager(new EfReservationDal());
        //REFACTORİNG EYLENECEK

        private readonly UserManager<AppUser> _userManager;

        public ReservationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> MyReservations()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valueList = _reservationManager.GetListAll(values.Id).OrderByDescending(x=>x.ReservationTime).ToList();
            ViewBag.table = valueList;

            var valueList1 = _reservationManager.GetListPreviousReservations(values.Id).OrderByDescending(x => x.ReservationTime).ToList();
            ViewBag.table1 = valueList1;

            return View();
        }
        [HttpGet]
        public IActionResult NewReservation()
        {
            List<SelectListItem> values = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-- Select Destination --", Selected = true }
            };
            values.AddRange(from x in _destinationManager.GetList()
                            select new SelectListItem
                            {
                                Text = x.City,
                                Value = x.DestinationID.ToString()
                            });
            ViewBag.values = values;
            return View();
        }

        [HttpPost]
        public IActionResult NewReservation(Reservation r, int Destination)
        {
            NewReservationValidator validationRules = new NewReservationValidator();
            FluentValidation.Results.ValidationResult result = validationRules.Validate(r);
            if (result.IsValid)
            {
                r.AppUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                r.DestinationID = Destination;
                r.Status = "Waiting for approval";//başlangıçta onay bekliyo olcak sonra bunu onaylicaklar
                _reservationManager.TInsert(r);
                TempData["SuccessMessage"] = "Reservation created successfully! Stay tuned for updates for the approval process, check status on the current reservation page.";
                return RedirectToAction("MyReservations", "Reservation", new { area = "Member" });
            }
            else
            {
                foreach (var items in result.Errors)
                {
                    ModelState.AddModelError("", items.ErrorMessage);
                }
                TempData["ErrorMessage"] = "There was an error occurred in the process!";
            }
 
            List<SelectListItem> values = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-- Select Destination --", Selected = true }
            };
            values.AddRange(from x in _destinationManager.GetList()
                            select new SelectListItem
                            {
                                Text = x.City,
                                Value = x.DestinationID.ToString()
                            });
            ViewBag.values = values;
            return View(r);
        }

    }
}