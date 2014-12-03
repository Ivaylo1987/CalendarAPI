namespace CalendarAPI.Infrastructure.FileUpload.Contracts
{
    using System.IO;
    using System.Web;

    public interface IFileManager
    {
        void SaveFile(Stream inputStream, string serverPath, string fileName);
        StreamReader GetFile(string filePath);
    }
}
