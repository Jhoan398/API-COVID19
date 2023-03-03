using Microsoft.AspNetCore.Mvc.ApiExplorer;
using static API_COVID19.Models.ApiResponse;

namespace API_COVID19.Models
{
    public class ResponseHandler
    {
        public static ApiResponse GetExceptionResponse(Exception e)
        {
            var response = new ApiResponse();
            response.Code = "1";
            response.Message = e.Message;

            return response;

        }

        public static ApiResponse GetAppResponse(ResponseType type, object? contract = null)      
        {
            ApiResponse response = new ApiResponse
            {
                ResponseData = contract,

            };

            switch (type) 
            {
                case ResponseType.Succes:
                    response.Code = "0";
                    response.Message = "Success";
                    break;

                case ResponseType.NotFound:
                    response.Code = "2";
                    response.Message = "No record available";
                    break;
            }

            return response;
        }
    }
}
