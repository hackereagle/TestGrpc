using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CalculatorInstance.BusinessLogics
{
    internal struct Trigger
    {
        public Trigger(string param, CalculatorEvent evnt) 
        {
            Param = param;
            Event = evnt;
        }

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
        private ActionBlock<Trigger> _fsmExecuteBlock;
        public CalculatorFsmManager() 
        { 
            _stateTransitionTable = new Dictionary<CalculatorState, List<Transition>>();
            _fsmActions = new CalculatorFsmAction();
            _fsmExecuteBlock = new ActionBlock<Trigger>(trigger => 
            { 

            });
        }


        public void Initialize()
        {
            foreach (var en in (CalculatorState[])Enum.GetValues(typeof(CalculatorState)))
                _stateTransitionTable.Add(en, new List<Transition>());

            _stateTransitionTable[CalculatorState.Idle].Add(new Transition(CalculatorEvent.PressNumEvent, _fsmActions.AppendNum, CalculatorState.InputFirstNum));
            ///////////
            _stateTransitionTable[CalculatorState.InputFirstNum].Add(new Transition(CalculatorEvent.PressNumEvent, _fsmActions.AppendNum, CalculatorState.InputFirstNum));
            _stateTransitionTable[CalculatorState.InputFirstNum].Add(new Transition(CalculatorEvent.PressOperatorEvent, _fsmActions.Store2First, CalculatorState.InputFirstNum));
            _stateTransitionTable[CalculatorState.InputFirstNum].Add(new Transition(CalculatorEvent.PressOtherEvent, _fsmActions.Calculate, CalculatorState.DisplayResult));
            ///////////
            _stateTransitionTable[CalculatorState.WaitSecondNum].Add(new Transition(CalculatorEvent.PressOperatorEvent, _fsmActions.DoNothing, CalculatorState.WaitSecondNum));
            _stateTransitionTable[CalculatorState.WaitSecondNum].Add(new Transition(CalculatorEvent.PressNumEvent, _fsmActions.StoreOperator, CalculatorState.InputSecondNum));
            ///////////
            _stateTransitionTable[CalculatorState.InputSecondNum].Add(new Transition(CalculatorEvent.PressNumEvent, _fsmActions.AppendNum, CalculatorState.InputSecondNum));
            _stateTransitionTable[CalculatorState.InputSecondNum].Add(new Transition(CalculatorEvent.PressOperatorEvent, _fsmActions.CalculateAndStore2First, CalculatorState.WaitSecondNum));
            _stateTransitionTable[CalculatorState.InputSecondNum].Add(new Transition(CalculatorEvent.PressEqualEvent, _fsmActions.Calculate, CalculatorState.DisplayResult));
        }

        #region Properties
        public IObservable<string> CurrentExpression => throw new NotImplementedException();

        public IObservable<string> Value => throw new NotImplementedException();

        public IObservable<CalculatorState> StateObservable => _fsmActions.StateNotification;
        #endregion Properties

        public async Task ReceiveNumberCommand(string str)
        {
            await Task.Run(() =>
            { 
                _fsmExecuteBlock.Post(new Trigger(str, CalculatorEvent.PressNumEvent)); 
            });
        }

        public async Task ReceiveOperatorCommand(string str)
        {
            await Task.Run(() =>
            {
                if (str == "=")
                    _fsmExecuteBlock.Post(new Trigger(str, CalculatorEvent.PressEqualEvent));
                else
                    _fsmExecuteBlock.Post(new Trigger(str, CalculatorEvent.PressOperatorEvent));
            });
        }

        public async Task ReceiveOtherCommand(string str)
        {
            await Task.Run(() =>
            {
                if (str == "CE")
                    _fsmExecuteBlock.Post(new Trigger(str, CalculatorEvent.PressAcButtonEvent));
                else
                    _fsmExecuteBlock.Post(new Trigger(str, CalculatorEvent.PressOtherEvent));
            });
        }
    }
}
