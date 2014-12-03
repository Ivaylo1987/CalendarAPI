namespace CalendarAPI.Infrastructure.CalendarServiceManager
{
    using CalendarAPI.GoogleServices;
    using CalendarAPI.GoogleServices.Contracts;
    using CalendarAPI.Infrastructure.CalendarServiceManager.Contracts;
    using CalendarAPI.Infrastructure.FileUpload;
    using CalendarAPI.Infrastructure.FileUpload.Contracts;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CalendarServiceManager : CalendarServiceManagerBase
    {
        private const string CalendarSummery = "Service Owned Calendar";
        private const string EventTitleFormat = "{0} has a BirthDay today. Going {1} !";
        private IFileParser fileParser;

        // poor man's IoC - use if no dependency container is available
        public CalendarServiceManager()
            :this(new FileParser())
        {
        }

        public CalendarServiceManager(IFileParser fileParser)
            : base(new CalendarServiceInitializer(), new TextFileManager())
        {
            this.fileParser = fileParser;
        }

        public void CreateBirhtDayEvents(string filePath, string email)
        {
            var calendarId = this.GetCalendarId(CalendarSummery);

            var isEmailInCalendarAcl = this.CheckIfEmailIsInCalendar(calendarId, email);

            if (!isEmailInCalendarAcl)
            {
                this.AddEmailToCalendar(calendarId, email);
            }

            var birthDays = this.fileParser.ParseBirthDays(filePath);

            this.CreateBirhtDayEvents(birthDays, calendarId);
        }

        private void CreateBirhtDayEvents(IEnumerable<IBirthDay> birthDays, string calendarId)
        {
            foreach (var birthDay in birthDays)
            {
                var eventDate = new DateTime(DateTime.Now.Year, birthDay.Date.Month, birthDay.Date.Day);
                var summery = string.Format(EventTitleFormat, birthDay.Name, DateTime.Now.Year - birthDay.Date.Year);

                var isEventPresent = this.CheckIsEventPresent(calendarId, eventDate, summery);

                if (!isEventPresent)
                {
                    var bdEvent = new Event()
                    {
                        Summary = summery,
                        Start = new EventDateTime()
                        {
                            Date = eventDate.ToString("yyyy-MM-dd")
                        },
                        End = new EventDateTime()
                        {
                            Date = eventDate.AddDays(1.0).ToString("yyyy-MM-dd")
                        }
                    };

                    this.CalendarService.Events.Insert(bdEvent, calendarId).Execute();
                }
            }
        }

        private bool CheckIsEventPresent(string calendarId, DateTime eventDate, string summery)
        {
            var calendarEvents = this.CalendarService.Events.List(calendarId);
            calendarEvents.TimeMax = eventDate.AddDays(1.0);
            calendarEvents.TimeMin = eventDate.AddDays(-1.0);

            var eventsInTheSameInterval = calendarEvents.Execute().Items;

            var isPresent = false;

            foreach (var item in eventsInTheSameInterval)
            {
                if (item.Summary == summery)
                {
                    isPresent = true;
                }
            }

            return isPresent;
        }
    }
}
