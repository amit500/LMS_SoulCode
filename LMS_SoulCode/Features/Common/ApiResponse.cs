namespace LMS_SoulCode.Features.Common
{

    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Code = StatusCodes.Success,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Fail(string message, int code)
        {
            return new ApiResponse<T>
            {
                Code = code,
                Message = message,
                Data = default
            };
        }
    }
}
