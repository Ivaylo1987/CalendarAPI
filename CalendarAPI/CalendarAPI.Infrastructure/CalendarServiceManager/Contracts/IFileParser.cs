namespace CalendarAPI.Infrastructure.CalendarServiceManager.Contracts
{
    using System.Collections.Generic;

    public interface IFileParser
    {
        IEnumerable<IBirthDay> ParseBirthDays(string filePath);
    }
}
