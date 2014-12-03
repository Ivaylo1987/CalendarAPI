namespace CalendarAPI.Infrastructure.FileUpload
{
    using CalendarAPI.Infrastructure.FileUpload.Contracts;
    using System.IO;
    using System.Web;

    public class TextFileManager : IFileManager
    {
        public void SaveFile(Stream inputStream, string serverPath, string fileName)
        {
            var filePath = Path.Combine(serverPath, fileName);
            using (var fileStream = File.Create(filePath))
            {
                inputStream.CopyTo(fileStream);
            }
        }

        public StreamReader GetFile(string filePath)
        {
            var fileStream = new StreamReader(filePath);
            return fileStream;
        }
    }
}
