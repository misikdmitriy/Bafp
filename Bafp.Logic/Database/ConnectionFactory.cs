using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Bafp.Logic.Database
{
    public interface IConnectionFactory
    {
        DbConnection Create(string db);
    }

    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IDictionary<string, string> _connectionStrings;

        public ConnectionFactory(IDictionary<string, string> connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public DbConnection Create(string db)
        {
            return new SqlConnection(_connectionStrings[db]);
        }
    }
}
