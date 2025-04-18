namespace HackerNewsApi.Models
{
    public interface IResponse
    {
        bool Success { get; }
        string Message { get; }
        ResponseData Data { get; }

        void SetError(Exception ex);
        void SetError(string message);
        void SetData(ResponseData message);
    }
}
