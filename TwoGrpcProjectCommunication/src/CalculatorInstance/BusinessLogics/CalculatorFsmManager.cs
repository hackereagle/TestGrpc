using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorInstance.BusinessLogics
{
    internal struct Trigger
    {
        public Trigger() { }

        public string Param = "";
        public CalculatorEvent Event = CalculatorEvent.NoEvent;
    }

    internal class Transition
    {
        public Transition(CalculatorEvent e, Action<Trigger> action, CalculatorState nextState) 
        {
            _event = e;
            _action = action;
            _nextState = nextState;
        }

        #region Fields
        private CalculatorEvent _event;
        private Action<Trigger> _action;
        private CalculatorState _nextState;
        #endregion Fields

        #region Properties
        private CalculatorEvent Event => _event;
        private Action<Trigger> Action => _action;
        private CalculatorState NextState => _nextState;
        #endregion Properties
    }

    internal class CalculatorFsmManager : CalculatorInstance.Bases.ICalculatorBusinessLogic
    {
        private Dictionary<CalculatorState, List<Transition>> _stateTransitionTable;
        private CalculatorFsmAction _fsmActions;
        public CalculatorFsmManager() 
        { 
            _stateTransitionTable = new Dictionary<CalculatorState, List<Transition>>();
            _fsmActions = new CalculatorFsmAction();
        }


        public void Initialize()
        {
            foreach (var en in (CalculatorState[])Enum.GetValues(typeof(CalculatorState)))
                _stateTransitionTable.Add(en, new List<Transition>());

            _stateTransitionTable[CalculatorState.Idle].Add(new Transition(CalculatorEvent.PressNumEvent, _fsmActions.AppendNum, CalculatorState.InputFirstNum));
            ///////////
            _stateTransitionTable[CalculatorState.InputFirstNum].Add(new Transition(CalculatorEvent.PressNumEvent, _fsmActions.AppendNum, CalculatorState.InputFirstNum));
            _stateTransitionTable[CalculatorState.InputFirstNum].Add(new Transition(CalculatorEvent.PressOperatorEvent, _fsmActions.Store2First, CalculatorState.InputFirstNum));
            _stateTransitionTable[CalculatorState.InputFirstNum].Add(new Transition(CalculatorEvent.PressOtherButtonEvent, _fsmActions.Calculate, CalculatorState.DisplayResult));
            ///////////
        }

        #region Properties
        public IObservable<string> CurrentExpression => throw new NotImplementedException();

        public IObservable<string> Value => throw new NotImplementedException();
        #endregion Properties

        public Task ReceiveNumberCommand(string str)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveOperatorCommand(string str)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveOtherCommand(string str)
        {
            throw new NotImplementedException();
        }
    }
}
