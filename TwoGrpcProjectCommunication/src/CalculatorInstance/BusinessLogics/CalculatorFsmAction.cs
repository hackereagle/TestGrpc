using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInstance.BusinessLogics
{
    internal class CalculatorFsmAction
    {
        private ComputeServiceProxy.ComputeServiceProxy _computeServiceProxy;
        public CalculatorFsmAction() 
        { 
            this._computeServiceProxy = new ComputeServiceProxy.ComputeServiceProxy();
        }

        public void AppendNum(Trigger trigger)
        { 
        }

        public void Store2First(Trigger trigger)
        { 
        }

        public void ClearAndAppend(Trigger trigger)
        { 
        }

        public void DoNothing(Trigger trigger)
        { }

        public void StoreOperator(Trigger trigger)
        { }

        public void Calculate(Trigger trigger)
        { }

        public void ClearAction(Trigger trigger)
        { }

    }
}
