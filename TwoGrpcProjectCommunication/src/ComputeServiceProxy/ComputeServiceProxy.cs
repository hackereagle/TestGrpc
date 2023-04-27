using CalculatorInstance;
using SclinMiscLib;

namespace ComputeServiceProxy
{
    public class ComputeServiceProxy
    {
        private ComputeService.ComputeServiceClient _computeService;

        public ComputeServiceProxy()
        {
            // If execute service via executable, using this code.
            //var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5000");
            // If click debug button, port will be different. So must be using this.
            var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5056");
            this._computeService = new ComputeService.ComputeServiceClient(channel);
        }

        public string Calculate(string num1, string opt, string num2)
        {
            var request = new CalculateRequest
            {
                FirstNum = num1,
                Operator = opt,
                SecondNum = num2,
            };

            CalculateResult result;
            try
            {
                result = _computeService.Arithmetic(request);
            }
            catch (Grpc.Core.RpcException ex)
            { 
                ConsoleWriterWrapper.Log($"In Caculate(string, string, string), call ComputeService.Arithmetic occur error: {ex.StatusCode.ToString()}\n", ex);
                result = new CalculateResult
                {
                    Result = "NaN"
                };
            }
            catch (Exception ex)
            {
                ConsoleWriterWrapper.Log($"In Caculate(string, string, string), call ComputeService.Arithmetic occur error", ex);
                result = new CalculateResult
                {
                    Result = "NaN"
                };
            }

            return result.Result;
        }

        public string Calculate(string num1, string opt)
        {
            var request = new CalculateRequest
            {
                FirstNum = num1,
                Operator = opt,
                SecondNum = "",
            };
            CalculateResult result;
            try
            {
                result = _computeService.OperatorCalculate(request);
            }
            catch (Grpc.Core.RpcException ex)
            { 
                ConsoleWriterWrapper.Log($"In Caculate(string, string, string), call ComputeService.Arithmetic occur error: {ex.StatusCode.ToString()}\n", ex);
                result = new CalculateResult
                {
                    Result = "NaN"
                };
            }
            catch (Exception ex)
            {
                ConsoleWriterWrapper.Log($"In Caculate(string, string), call ComputeService.Arithmetic occur error", ex);
                result = new CalculateResult
                {
                    Result = "NaN"
                };
            }

            return result.Result;
        }
    }
}