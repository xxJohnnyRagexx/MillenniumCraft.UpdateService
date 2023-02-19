namespace UpdateService.Filters
{
    public class ExceptionResponse
    {
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
