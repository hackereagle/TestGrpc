﻿using CalculatorInstance;

namespace ComputeServiceProxy
{
    public class ComputeServiceProxy
    {
        private ComputeService.ComputeServiceClient _computeService;

        public ComputeServiceProxy()
        { 
            var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5000");
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
            var result = _computeService.Arithmetic(request);

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
            var result = _computeService.OperatorCalculate(request);

            return result.Result;
        }
    }
}