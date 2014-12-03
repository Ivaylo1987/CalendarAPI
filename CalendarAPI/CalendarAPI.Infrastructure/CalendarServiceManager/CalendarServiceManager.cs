namespace CalendarAPI.Infrastructure.CalendarServiceManager
{
    using CalendarAPI.GoogleServices;
    using CalendarAPI.GoogleServices.Contracts;
    using CalendarAPI.Infrastructure.FileUpload;
    using CalendarAPI.Infrastructure.FileUpload.Contracts;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using System.Linq;

    public class CalendarServiceManager : CalendarServiceManagerBase
    {
        private const string CalendarSummery = "Service Owned Calendar";

        // poor man's IoC - use if no dependency container is available
        public CalendarServiceManager()
            : base(new CalendarServiceInitializer(), new TextFileManager())
        {
        }

        public void CreateBirhtDayEvents(string filePath, string email)
        {
            var calendarId = this.GetCalendarId(CalendarSummery);

            var isEmailInCalendarAcl = this.CheckIfEmailIsInCalendar(calendarId, email);

            if (!isEmailInCalendarAcl)
            {
                this.AddEmailToCalendar(calendarId, email);
            }

            var fileStream = this.FileManager.GetFile(filePath);
        }
    }
}
