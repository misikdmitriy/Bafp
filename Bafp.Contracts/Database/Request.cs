using System;

namespace Bafp.Contracts.Database
{
    public class Request
    {
        public string ProcedureName { get; set; }
        public object Parameter { get; set; }

        public Response<TOut> Ok<TOut>(TOut result)
        {
            return new Response<TOut>
            {
                Request = this,
                Result = result,
                Success = true
            };
        }

        public Response<TOut> Fail<TOut>(Exception ex)
        {
            return new Response<TOut>
            {
                Request = this,
                Success = false,
                Exception = ex,
                Message = ex.Message
            };
        }

        public Response<TOut> Fail<TOut>(string message)
        {
            return new Response<TOut>
            {
                Request = this,
                Success = false,
                Message = message
            };
        }
    }

    public class Response<TOut>
    {
        public Request Request { get; set; }
        public bool Success { get; set; }
        public TOut Result { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    public class Null
    {
    }
}
