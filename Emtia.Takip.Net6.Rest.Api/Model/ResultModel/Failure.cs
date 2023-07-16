namespace Emtia.Takip.Net6.Rest.Api.Model.ResultModel
{
    public class Failure<T> : Response<T>
    {
        public Failure(string comment, string message = "Hata")
        {
            Result = false;
            ResultCode = -1;
            Message = message;
            Comment = comment;
        }
    }
}