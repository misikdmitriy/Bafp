using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bafp.Contracts.Database;
using Bafp.Logic.Database;

namespace Bafp.Logic.Services
{
    public interface IDatabaseService
    {
        Task<DbResponse<IEnumerable<TOut>>> ExecuteStoredProcedure<TOut>(object input);
    }

    public class DatabaseService : IDatabaseService
    {
        private readonly IMapper _mapper;
        private readonly IConnectionFactory _connectionFactory;

        public DatabaseService(IMapper mapper, IConnectionFactory connectionFactory)
        {
            _mapper = mapper;
            _connectionFactory = connectionFactory;
        }

        public async Task<DbResponse<IEnumerable<TOut>>> ExecuteStoredProcedure<TOut>(object input)
        {
            var request = _mapper.Map<DbRequest>(input);

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    var executor = new SpExecutor(connection);
                    var result = await executor.Execute<TOut>(request.ProcedureName, request.ParameterResolver());
                    return request.Ok(result);
                }
            }
            catch (Exception e)
            {
                return request.Fail<IEnumerable<TOut>>(e);
            }
        }
    }
}
