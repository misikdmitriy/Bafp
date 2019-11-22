using System;

namespace Bafp.Contracts.Database
{
    public class Request
    {
        public string ProcedureName { get; set; }
        public object Parameter { get; set; }
    }

    public class Response<T>
    {
        public Request Request { get; set; }
        public bool Success { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    public class Null
    {
    }
}
