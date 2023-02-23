namespace API_COVID19.Models
{
    public class ApiResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object? ResponseData { get; set; }


        public enum ResponseType
        {
            Succes,
            NotFound,
            Failure
        }
    }

}
