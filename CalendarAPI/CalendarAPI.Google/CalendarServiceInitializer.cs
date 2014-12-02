using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace CalendarAPI.Google
{
    public class CalendarServiceInitializer
    {
        private const string ServiceAccountEmail = "847753983802-6qbjred3ht7beabof68s9dapuq94912i@developer.gserviceaccount.com";
        private const string GoogleApplicationName = "CalendarApiTest";
        private const string CertificateRelativePath = "../../CalendarAPI.Google/Certificate/CalendarApiTest-a03ab29c21cd.p12";
        private X509Certificate2 serviceCertificate;

        public CalendarServiceInitializer()
        {
            var fullPath = this.GetFullPathToCertificate();
            this.serviceCertificate = new X509Certificate2(fullPath, "notasecret", X509KeyStorageFlags.Exportable);
        }

        public CalendarService GetCalendarService()
        {
            var credentils = this.GetCredentials();

            var calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentils,
                ApplicationName = GoogleApplicationName
            });

            return calendarService;
        }

        private ServiceAccountCredential GetCredentials()
        {
            var credentials = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(ServiceAccountEmail)
                {
                    Scopes = new[] { CalendarService.Scope.Calendar }
                }.FromCertificate(this.serviceCertificate));

            return credentials;
        }

        private string GetFullPathToCertificate()
        {
            var appDomain = AppDomain.CurrentDomain;
            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
            var combinedPath = Path.Combine(basePath, CertificateRelativePath);

            // normalizes the absolute and the relative path combination
            var fullPath = Path.GetFullPath(combinedPath);

            return fullPath;
        }
    }
}
