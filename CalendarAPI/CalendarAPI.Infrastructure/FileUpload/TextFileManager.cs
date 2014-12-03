namespace CalendarAPI.Infrastructure.FileUpload
{
    using CalendarAPI.Infrastructure.FileUpload.Contracts;
    using System.IO;

    public class TextFileManager : IFileManager
    {
        public void SaveFile(Stream inputStream, string filePath)
        {
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
