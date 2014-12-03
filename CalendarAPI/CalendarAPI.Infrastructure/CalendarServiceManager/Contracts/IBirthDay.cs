namespace CalendarAPI.Infrastructure.CalendarServiceManager.Contracts
{
    using System;

    public interface IBirthDay
    {
        string Name { get; set; }

        DateTime Date { get; set; }
    }
}
