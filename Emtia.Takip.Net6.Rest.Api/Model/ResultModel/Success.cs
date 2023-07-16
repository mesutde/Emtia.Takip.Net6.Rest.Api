namespace Emtia.Takip.Net6.Rest.Api.Model.ResultModel
{
    public class Success<T> : Response<T>
    {
        public Success(T data, string message, string comment)
        {
            Result = true;
            ResultCode = 200;
            Message = message;
            Comment = comment;
            Data = data;
            UpdateTime = DateTime.Now.ToString();
        }
    }
}