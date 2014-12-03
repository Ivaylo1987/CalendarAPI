namespace CalendarAPI.Infrastructure.CalendarServiceManager
{
    using CalendarAPI.Infrastructure.CalendarServiceManager.Contracts;
    using System;

    class BirthDay : IBirthDay
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }
    }
}
