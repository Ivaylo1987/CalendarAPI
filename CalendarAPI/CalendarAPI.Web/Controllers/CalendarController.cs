namespace CalendarAPI.Web.Controllers
{
    using CalendarAPI.Google;
    using CalendarAPI.Infrastructure.FileUpload;
    using CalendarAPI.Infrastructure.FileUpload.Contracts;
    using CalendarAPI.Web.Models.Calendar;
    using System.IO;
    using System.Web.Configuration;
    using System.Web.Mvc;

    public class CalendarController : Controller
    {
        private IFileUploader fileUploader;

        // poor man's IoC use if no dependency container is available
        public CalendarController()
            : this(new TextFileUploader())
        {
        }

        public CalendarController(IFileUploader fileUploader)
        {
            this.fileUploader = fileUploader;
        }

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
                TempData["errorMessage"] = "You have submited Invalid Data!";
            }
            else
            {
                var fileVirtualDirectory = WebConfigurationManager.AppSettings["TextFilesVirtualDirectory"];
                var serverPath = Server.MapPath(fileVirtualDirectory);

                this.fileUploader.SaveFile(model.UploadedFile, serverPath);
                TempData["successMessage"] = "File was succesfully uploaded.";

                // testing the service
                var calendarService = new CalendarServiceInitializer().GetCalendarService();
            }

            return RedirectToAction("Submit", "Calendar");
        }
    }
}