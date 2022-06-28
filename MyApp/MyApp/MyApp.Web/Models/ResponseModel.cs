namespace MyApp.Web.Models
{
    public enum ResponseType
    {
        Success,
        Danger,
        Warning
    }

    public class ResponseModel
    {
        public ResponseType? Type { get; set; }
        public string? Message { get; set; }
    }
}
