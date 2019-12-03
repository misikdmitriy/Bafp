using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bafp.Contracts;
using Bafp.Contracts.Database;
using Bafp.Logic.Database;
using Serilog;

namespace Bafp.Logic.Services
{
    public interface IDatabaseService
    {
        Task<DbResponse<TOut[]>> ExecuteStoredProcedure<TOut>(DbRequest request);
    }

    public class DatabaseService : IDatabaseService
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger _logger = Log.ForContext<DatabaseService>();

        public DatabaseService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<DbResponse<TOut[]>> ExecuteStoredProcedure<TOut>(DbRequest request)
        {
            _logger.Information("Executing DB request {@request}", request);
            
            try
            {
                using (var connection = _connectionFactory.Create(Constants.DatabaseNames.MsSql))
                {
                    var executor = new SpExecutor(connection);
                    var result = (await executor.Execute<TOut>(request.ProcedureName, request.Parameter))
                        .ToArray();
                    
                    _logger.Information("DB response {@response}", result);
                    
                    return request.Ok(result);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error occured during executing DB request {@request}", request);
                return request.Fail<TOut[]>(e);
            }
        }
    }
}
