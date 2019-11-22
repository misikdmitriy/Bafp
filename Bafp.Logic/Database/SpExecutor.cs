using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;

namespace Bafp.Logic.Database
{
    public interface ISpExecutor
    {
        Task<IEnumerable<T>> Execute<T>(string spName, object param);
    }

    public class SpExecutor : ISpExecutor
    {
        private readonly DbConnection _connection;

        public SpExecutor(DbConnection connection)
        {
            _connection = connection;
        }

        public Task<IEnumerable<T>> Execute<T>(string spName, object param)
        {
            return _connection.QueryAsync<T>(spName, param, commandType: CommandType.StoredProcedure);
        }
    }
}
