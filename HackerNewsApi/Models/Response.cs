namespace HackerNewsApi.Models
{
    /// <summary>
    /// Base class for every response
    /// </summary>
    public class Response : IResponse
    {
        /// <summary>
        /// Gets the Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets the Status
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets the result of the response
        /// </summary>
        public ResponseData Data { get; set; } = new ResponseData();

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="errorMessage"></param>
        public Response(string errorMessage)
        {
            this.SetError(errorMessage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="exception"></param>
        public Response(Exception exception)
        {
            this.SetError(exception);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="data"></param>
        public Response(ResponseData data)
        {
            this.SetData(data);
        }

        /// <summary>
        /// Set the error of the response
        /// </summary>
        /// <param name="message"></param>
        public void SetError(string message)
        {
            Data = new ResponseData();
            Success = false;
            Message = message;
        }

        /// <summary>
        /// Set the error of the response
        /// </summary>
        /// <param name="ex"></param>
        public void SetError(Exception ex)
        {
            Data = new ResponseData();
            Success = false;
            Message = ex.ToString();
        }

        /// <summary>
        /// Set the data of the response
        /// </summary>
        /// <param name="data"></param>
        public void SetData(ResponseData data)
        {
            Data = data;
            Success = true;
            Message = "";
        }
    }

}
