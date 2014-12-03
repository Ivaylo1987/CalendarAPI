namespace CalendarAPI.Infrastructure.CalendarServiceManager
{
    using CalendarAPI.GoogleServices.Contracts;
    using CalendarAPI.Infrastructure.FileUpload.Contracts;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using System.Linq;

    public abstract class CalendarServiceManagerBase
    {
        public CalendarServiceManagerBase(ICalendarServiceInitializer calendarServiceInitializer, IFileManager fileManager)
        {
            this.CalendarService = calendarServiceInitializer.GetCalendarService();
            this.FileManager = fileManager;
        }

        public CalendarService CalendarService { get; set; }

        public IFileManager FileManager { get; set; }

        protected string GetCalendarId(string calendarSummery)
        {
            var calendarId = string.Empty;
            var calendarList = CalendarService.CalendarList.List().Execute().Items;

            if (calendarList.Count <= 0)
            {
                calendarId = this.CreateCalendar(calendarSummery).Id;
            }
            else
            {
                calendarId = calendarList.FirstOrDefault(c => c.Summary == calendarSummery).Id;
            }

            return calendarId;
        }

        protected Calendar CreateCalendar(string calendarSummery)
        {
            var cal = new Calendar()
            {
                Summary = calendarSummery
            };

            var createdCalendar = CalendarService.Calendars.Insert(cal).Execute();

            return createdCalendar;
        }

        protected bool CheckIfEmailIsInCalendar(string calendarId, string email)
        {
            var aclList = CalendarService.Acl.List(calendarId).Execute().Items;
            bool isEmailInAclLists = false;

            foreach (var rule in aclList)
            {
                if (rule.Scope != null)
                {
                    var scope = rule.Scope;
                    if (scope.Type == "user" && scope.Value == email)
                    {
                        isEmailInAclLists = true;
                    }
                }
            }

            return isEmailInAclLists;
        }

        protected void AddEmailToCalendar(string calendarId, string email)
        {
            AclRule userRule = new AclRule()
            {
                Role = "reder",
                Scope = new AclRule.ScopeData()
                {
                    Type = "user",
                    Value = email
                }
            };

            CalendarService.Acl.Insert(userRule, calendarId).Execute();
        }
    }
}
