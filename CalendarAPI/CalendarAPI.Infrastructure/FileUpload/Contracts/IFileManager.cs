namespace CalendarAPI.Infrastructure.FileUpload.Contracts
{
    using System.IO;
    using System.Web;

    public interface IFileManager
    {
        void SaveFile(Stream inputStream, string filePath);
        StreamReader GetFile(string filePath);
    }
}
