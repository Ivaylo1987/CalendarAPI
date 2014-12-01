namespace CalendarAPI.Web.Controllers
{
    using CalendarAPI.Web.Models.Calendar;
    using System.IO;
    using System.Web.Mvc;

    public class CalendarController : Controller
    {
        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(CalendarSubmitModel model)
        {
            if (!ModelState.IsValid)
            {
                // error
            }

            return RedirectToAction("Submit", "Calendar");
        }
    }
}