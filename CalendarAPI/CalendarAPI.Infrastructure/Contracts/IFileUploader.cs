namespace CalendarAPI.Infrastructure.Contracts
{
    using System.Web;

    public interface IFileUploader
    {
        void SaveFile(HttpPostedFileBase file, string serverPath);
    }
}
