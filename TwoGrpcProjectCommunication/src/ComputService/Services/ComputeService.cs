using CalculatorInstance;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace CalculatorInstance.Services
{
    public class ComputeService : CalculatorInstance.ComputeService.ComputeServiceBase
    {
        private readonly ILogger<ComputeService> _logger;
        public ComputeService(ILogger<ComputeService> logger) 
        {
            _logger = logger;
        }

        private T CalculateInstance<T>(T n1, T n2, string opt)
        {
            dynamic _n1 = n1;
            dynamic _n2 = n2;
            dynamic result = -9999999999999;
            if (opt == "+")
            {
                result = _n1 + _n2;
                return result;
            }
            else if (opt == "-")
            {
                result = _n1 - _n2;
                return result;
            }
            else if (opt == "*")
            {
                result = _n1 * _n2;
                return result;
            }
            else if (opt == "/")
            {
                result = _n1 / _n2;
                return result;
            }
            else
            {
                throw new ArgumentException("Undefined operator!");
            }
        }

        public override Task<CalculateResult> Arithmetic(CalculateRequest request, ServerCallContext context)
        {
            return Task.Run<CalculateResult>(() => 
            {
                CalculateResult result = new CalculateResult();

                string num1 = request.FirstNum;
                string num2 = request.SecondNum;
                string opt = request.Operator;
                bool byDouble = num1.IndexOf(".") != -1 || num2.IndexOf(".") != -1;

                if (byDouble)
                {
                    double n1 = double.Parse(num1);
                    double n2 = double.Parse(num2);
                    result.Result = $"{CalculateInstance<double>(n1, n2, opt)}";
                }
                else
                {
                    int n1 = int.Parse(num1);
                    int n2 = int.Parse(num2);
                    result.Result = $"{CalculateInstance<int>(n1, n2, opt)}";
                }

                return result;
            });
        }

        private double CalculateInstance<T>(T n1, string opt)
        {
            double _n1 = Convert.ToDouble(n1);
            double result = -9999999999999.0;
            if (opt == "%")
            {
                result = Convert.ToDouble(_n1) / 100.0;
                return result;
            }
            else if (opt == "1/x")
            {
                result = 1.0 / _n1;
                return result;
            }
            else if (opt == "x^2")
            {
                result = Math.Pow(_n1, 2.0);
                return result;
            }
            else if (opt == "x^(1/2)")
            {
                result = Math.Sqrt(_n1);
                return result;
            }
            else
            {
                throw new ArgumentException("Undefined operator!");
            }
        }

        public override Task<CalculateResult> OperatorCalculate(CalculateRequest request, ServerCallContext context)
        {
            return Task.Run<CalculateResult>(() =>
            {
                CalculateResult result = new CalculateResult();

                string num1 = request.FirstNum;
                string opt = request.Operator;
                bool byDouble = num1.IndexOf(".") != -1;

                if (byDouble)
                {
                    double n1 = double.Parse(num1);
                    result.Result = $"{CalculateInstance<double>(n1, opt)}";
                }
                else
                {
                    int n1 = int.Parse(num1);
                    result.Result = $"{CalculateInstance<int>(n1, opt)}";
                }

                return result;
            });
        }
    }
}
