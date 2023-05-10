using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInstance.BusinessLogics
{
    public class State<TStateEnum> where TStateEnum : System.Enum
    { 
        public State(TStateEnum initialState) 
        {
            _subject = new Subject<TStateEnum>();
            _state = initialState;
        }

        private TStateEnum _state;
        private ISubject<TStateEnum> _subject;
        public IObservable<TStateEnum> Observerable() => _subject.AsObservable<TStateEnum>();
        public TStateEnum Value
        {
            set
            { 
                _state = value; 
                _subject.OnNext(_state);
            }
            get => _state;
        }
    }

    internal class CalculatorFsmAction
    {
        private ComputeServiceProxy.ComputeServiceProxy _computeServiceProxy;
        private State<CalculatorState> _curState;
        public CalculatorFsmAction() 
        { 
            this._computeServiceProxy = new ComputeServiceProxy.ComputeServiceProxy();
            _curState = new State<CalculatorState>(CalculatorState.Idle);
        }

        public IObservable<CalculatorState> StateNotification => _curState.Observerable();

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
        { 
        }

        public void StoreOperator(Trigger trigger)
        { 
        }

        public void Calculate(Trigger trigger)
        { 
        }

        public void ClearAction(Trigger trigger)
        { 
        }

        public void ExecuteFsm(CalculatorEvent evnt, Trigger trigger)
        { 
        }
    }
}
