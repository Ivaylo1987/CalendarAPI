namespace CalendarAPI.Web.Controllers
{
    using CalendarAPI.Infrastructure.CalendarServiceManager;
    using CalendarAPI.Infrastructure.FileUpload;
    using CalendarAPI.Infrastructure.FileUpload.Contracts;
    using CalendarAPI.Web.Models.Calendar;
    using System.IO;
    using System.Web.Configuration;
    using System.Web.Mvc;

    public class CalendarController : Controller
    {
        private IFileManager fileUploader;
        private CalendarServiceManager calendarServiceManager;

        // poor man's IoC use if no dependency container is available
        public CalendarController()
            : this(new TextFileManager(), new CalendarServiceManager())
        {
        }

        public CalendarController(IFileManager fileUploader, CalendarServiceManager calendarServiceManager)
        {
            this.fileUploader = fileUploader;
            this.calendarServiceManager = calendarServiceManager;
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
                var filePath = Path.Combine(directoryPath, model.UploadedFile.FileName);

                this.fileUploader.SaveFile(model.UploadedFile.InputStream, filePath);
                TempData["successMessage"] = "File was succesfully uploaded.";


                this.calendarServiceManager.CreateBirhtDayEvents(filePath, model.Email);
            }

            return RedirectToAction("Submit", "Calendar");
        }
    }
}