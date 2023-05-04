using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInstance.Bases
{
    public interface ICalculatorBusinessLogic
    {
        public IObservable<string> CurrentExpression { get; }
        public IObservable<string> Value { get; }

        public Task ReceiveNumberCommand(string str);

        public Task ReceiveOperatorCommand(string str);

        public Task ReceiveOtherCommand(string str);
    }
}
