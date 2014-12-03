using CalendarAPI.Google;
using CalendarAPI.Infrastructure.FileUpload;
using CalendarAPI.Infrastructure.FileUpload.Contracts;
using CalendarAPI.Web.Models.Calendar;
using Google.Apis.Calendar.v3;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CalendarAPI.Web.Controllers
{
    public class CalendarController : Controller
    {
        private IFileManager fileUploader;
        private CalendarService calendarService;

        // poor man's IoC use if no dependency container is available
        public CalendarController()
            : this(new TextFileManager(), new CalendarServiceInitializer().GetCalendarService())
        {
        }

        public CalendarController(IFileManager fileUploader, CalendarService calendarService)
        {
            this.fileUploader = fileUploader;
            this.calendarService = calendarService;
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
                var directoryPath = Server.MapPath(fileVirtualDirectory);

                this.fileUploader.SaveFile(model.UploadedFile.InputStream, directoryPath, model.UploadedFile.FileName);
                TempData["successMessage"] = "File was succesfully uploaded.";
            }

            return RedirectToAction("Submit", "Calendar");
        }
    }
}