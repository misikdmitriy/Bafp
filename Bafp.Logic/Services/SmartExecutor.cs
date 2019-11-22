using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bafp.Contracts.Database;
using Bafp.Logic.Database;

namespace Bafp.Logic.Services
{
    public interface ISmartExecutor
    {
        Task<Response<IEnumerable<TOut>>> ExecuteSp<TOut>(object input);
    }

    public class SmartExecutor : ISmartExecutor
    {
        private readonly IMapper _mapper;
        private readonly IConnectionFactory _connectionFactory;

        public SmartExecutor(IMapper mapper, IConnectionFactory connectionFactory)
        {
            _mapper = mapper;
            _connectionFactory = connectionFactory;
        }

        public async Task<Response<IEnumerable<TOut>>> ExecuteSp<TOut>(object input)
        {
            var request = _mapper.Map<Request>(input);

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    var executor = new SpExecutor(connection);
                    var result = await executor.Execute<TOut>(request.ProcedureName, request.Parameter);
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
