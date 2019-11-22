using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bafp.Contracts.Database;
using Bafp.Logic.Database;
using MediatR;

namespace Bafp.Logic.Mediatr
{
    public class ExecuteSpRequest<TIn, TOut> : IRequest<Response<TOut>>
    {
        public ExecuteSpRequest(TIn input)
        {
            Input = input;
        }

        public TIn Input { get; }
    }

    public class ExecuteSpHandler<TIn, TOut> : IRequestHandler<ExecuteSpRequest<TIn, TOut>, Response<TOut>>
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;

        public ExecuteSpHandler(IConnectionFactory connectionFactory, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public async Task<Response<TOut>> Handle(ExecuteSpRequest<TIn, TOut> req, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<Request>(req.Input);

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    var spExecutor = new SpExecutor(connection);
                    var result = await spExecutor.Execute(request.ProcedureName, request.Parameter);
                    return new Response<TOut>
                    {
                        Request = request,
                        Result = _mapper.Map<TOut>(result),
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                return new Response<TOut>
                {
                    Request = request,
                    Result = default,
                    Success = true,
                    Message = e.Message,
                    Exception = e
                };
            }
        }
    }
}
