namespace CalendarAPI.GoogleServices.Contracts
{
    using Google.Apis.Calendar.v3;

    public interface ICalendarServiceInitializer
    {
        CalendarService GetCalendarService();
    }
}
