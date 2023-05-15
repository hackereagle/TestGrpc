using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInstance.BusinessLogics
{
    internal enum CalculatorEvent : int
    { 
        NoEvent,
        PressNumEvent,
        PressOperatorEvent,
        PressEqualEvent,
        PressOtherEvent,
        PressAcButtonEvent,
    }
}
