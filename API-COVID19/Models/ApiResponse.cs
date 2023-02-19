namespace API_COVID19.Models
{
    public class APIResponse
    {
        public int Code { get; set; }
        public string  Message { get; set; }
        public int MyProperty { get; set; }

        public object? ResponseData { get; set; }


        public enum ResponseType
        {
            Succes,
            NotFound,
            Failure
        }
    }

}
