namespace CalendarAPI.Infrastructure.CalendarServiceManager
{
    using CalendarAPI.GoogleServices;
    using CalendarAPI.GoogleServices.Contracts;
    using Google.Apis.Calendar.v3;

    public class CalendarServiceManager
    {
        private CalendarService calendarService;

        // poor man's IoC - use if no dependency container is available
        public CalendarServiceManager()
            : this(new CalendarServiceInitializer())
        {
        }

        public CalendarServiceManager(ICalendarServiceInitializer calendarServiceInitializer)
        {
            this.calendarService = calendarServiceInitializer.GetCalendarService();
        }

        public void CreateBirhtDayEvents(string filePath)
        {

        }
    }
}
