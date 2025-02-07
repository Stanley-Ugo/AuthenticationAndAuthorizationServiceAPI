namespace AuthenticationAndAuthorization.Application.Utilities
{
    public class StandardResponse<T>
    {
        public bool Status { get; set; }

        public string Message { get; set; }
        public T? Data { get; set; }
        public string Code { get; set; }
        public object Errors { get; set; }


        public static StandardResponse<T> SuccessMessage(string code = "200", string message = "", dynamic data = null, bool status = true)
        {
            return new StandardResponse<T>
            {
                Data = (T)data,
                Message = (message ?? "Success"),
                Status = status,
                Code = code
            };
        }

        public static StandardResponse<T> ErrorMessage(string code, string message = "")
        {
            return new StandardResponse<T>
            {
                Message = (message ?? "Error"),
                Status = false,
                Code = code
            };
        }

        public static StandardResponse<T> TokenMessage()
        {
            return new StandardResponse<T>
            {
                Message = "TokenError",
                Status = false
            };
        }

        public static StandardResponse<T> SystemError(string message = "")
        {
            return new StandardResponse<T>
            {
                Message = (message ?? "Unknown Error Occurred. Please try again later"),
                Status = false,
                Code = "500"
            };
        }

    }
}
