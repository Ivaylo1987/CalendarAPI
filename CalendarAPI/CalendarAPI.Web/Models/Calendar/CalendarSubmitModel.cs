namespace CalendarAPI.Web.Models.Calendar
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class CalendarSubmitModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "File")]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}