namespace OrderAPI.Models.Results
{
    public class ApiResponse<T>
    {
        public StatusType Status { get; set; }
        public string ResultMessage { get; set; }
        public int ErrorCode { get; set; }
        public T Data { get; set; }
        public ApiResponse(StatusType status, T data)
        {
            Status = status;
            Data = data;
        }
        public ApiResponse(StatusType status ,string resultMesssage)
        {
            Status=status;
            ResultMessage = resultMesssage;
        }
        public ApiResponse(StatusType status,string resultMessage,T data)
        {
            Status = status;
            ResultMessage = resultMessage;
            Data = data;

        }
        public ApiResponse(StatusType status , string resultMesssage,int errorCode)
        {
            Status = status;
            ResultMessage = resultMesssage;
            ErrorCode = errorCode;
        }
       
    }
}
public enum StatusType
{
    Success, Failed
}
