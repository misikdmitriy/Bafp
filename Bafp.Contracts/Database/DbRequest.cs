using System;

namespace Bafp.Contracts.Database
{
    public class DbRequest
    {
        public string ProcedureName { get; set; }
        public object Parameter { get; set; }

        public DbResponse<TOut> Ok<TOut>(TOut result)
        {
            return new DbResponse<TOut>
            {
                DbRequest = this,
                Result = result,
                Success = true
            };
        }

        public DbResponse<TOut> Fail<TOut>(Exception ex)
        {
            return new DbResponse<TOut>
            {
                DbRequest = this,
                Success = false,
                Exception = ex,
                Message = ex.Message
            };
        }
    }

    public class DbResponse<TOut>
    {
        public DbRequest DbRequest { get; set; }
        public bool Success { get; set; }
        public TOut Result { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
