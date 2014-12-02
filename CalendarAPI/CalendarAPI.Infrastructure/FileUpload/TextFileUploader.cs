namespace CalendarAPI.Infrastructure.FileUpload
{
    using CalendarAPI.Infrastructure.FileUpload.Contracts;
    using System.IO;
    using System.Web;

    public class TextFileUploader : IFileUploader
    {
        public void SaveFile(HttpPostedFileBase file, string serverPath)
        {
            var fileName = file.FileName;
            var filePath = Path.Combine(serverPath, fileName);
            file.SaveAs(filePath);
        }
    }
}
