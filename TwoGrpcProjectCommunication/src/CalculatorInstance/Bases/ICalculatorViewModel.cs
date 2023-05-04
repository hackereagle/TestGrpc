using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatorInstance.Bases
{
    public interface ICalculatorViewModel
    {
        public ICommand NumberBtn { get; }
        public ICommand OperatorBtn { get; }
        public ICommand OtherBtn { get; }

        public string CurrentExpression { set; get; }

        public string Value { set; get; }
    }
}
