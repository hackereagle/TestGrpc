using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInstance.BusinessLogics
{
    public enum CalculatorState : int
    {
        Idle,
        InputFirstNum,
        WaitSecondNum,
        InputSecondNum,
        DisplayResult,
    }
}
