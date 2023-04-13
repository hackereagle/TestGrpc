using CalculatorInstance;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using CalculatorInstance;
using grpc = global::Grpc.Core;

namespace CalculatorInstance.Services
{
    public class ComputeService : CalculatorInstance.ComputeService.ComputeServiceBase
    {
        private readonly ILogger<ComputeService> _logger;
        public ComputeService(ILogger<ComputeService> logger) 
        {
            _logger = logger;
        }

        public override Task<CalculateResult> Arithmetic(CalculateRequest request, ServerCallContext context)
        {
            return Task.FromResult<CalculateResult>(new CalculateResult 
            {
                Result = $"yo~man! I receive request: {request.FirstNum} {request.Operator} {request.SecondNum}!"
            });
        }

        public override Task<CalculateResult> OperatorCalculate(CalculateRequest request, ServerCallContext context)
        {
            return Task.FromResult<CalculateResult>(new CalculateResult 
            {
                Result = $"What\'t up man! I recieve request: {request.FirstNum} do {request.Operator} operate!"
            });
        }
    }
}
