using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;

namespace Bafp.Logic.Database
{
    public interface ISpExecutor
    {
        Task<IEnumerable<object>> Execute(string spName, object param);
    }

    public class SpExecutor : ISpExecutor
    {
        private readonly DbConnection _connection;

        public SpExecutor(DbConnection connection)
        {
            _connection = connection;
        }

        public Task<IEnumerable<object>> Execute(string spName, object param)
        {
            return _connection.QueryAsync<object>(spName, param, commandType: CommandType.StoredProcedure);
        }
    }
}
