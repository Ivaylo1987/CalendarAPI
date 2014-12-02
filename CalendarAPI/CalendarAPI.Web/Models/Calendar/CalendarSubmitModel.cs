namespace CalendarAPI.Web.Models.Calendar
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using System.Web;

    public class CalendarSubmitModel
    {
        Regex regExInsensitive = new Regex(@"^[a-z0-9](\.?[a-z0-9]){1,}@g(oogle)?mail\.com$", RegexOptions.IgnoreCase);

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-z0-9](\.?[a-z0-9]){1,}@g(oogle)?mail\.com$", ErrorMessage = "You should provide a gmail.com address in lowercase.")]
        [Display(Name = "Google E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "File")]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}