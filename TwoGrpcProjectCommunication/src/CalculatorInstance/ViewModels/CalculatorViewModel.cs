using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorInstance.Base;
using System.Windows.Input;
using System.Windows.Controls;

namespace CalculatorInstance.ViewModel
{
    internal class CalculatorViewModel : Base.ViewModelBase
    {
        private AsyncCommand<object> _numberBtnCommand;
        private AsyncCommand<object> _operatorBtnCommand;
        private AsyncCommand<object> _otherBtnCommand;
        BusinessLogics.CalculatorLogic _calculatorLogic;
        public CalculatorViewModel()
        { 
            this._calculatorLogic = new BusinessLogics.CalculatorLogic();
            this._calculatorLogic.Value.Subscribe(x => 
            {
                this.Value = x;
                Enable = true;
            });
            this._calculatorLogic.CurrentExpression.Subscribe(x => { this.CurrentExpression = x; });

            this._numberBtnCommand = new AsyncCommand<object>(this.NumberButtonCommand, BtnCanExecute);
            this._operatorBtnCommand = new AsyncCommand<object>(this.OperatorButtonCommand, BtnCanExecute);
            this._otherBtnCommand = new AsyncCommand<object>(this.OtherButtonCommand, BtnCanExecute);
            Enable = true;
        }

        bool BtnCanExecute(object obj)
        {
            return !_isBusy;
        }

        public ICommand NumberBtn => _numberBtnCommand;
        public ICommand OperatorBtn => _operatorBtnCommand;
        public ICommand OtherBtn => _otherBtnCommand;

        private string _currentExpression;
        public string CurrentExpression
        {
            get { return _currentExpression; } 
            set
            {
                _currentExpression = value;
                OnPropertyChanged("CurrentExpression");
            }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            { 
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        bool _isBusy;
        public bool Enable
        {
            get { return !_isBusy; }
            set
            { 
                _isBusy = !value;
                OnPropertyChanged("Enable");
            }
        }

        private async Task NumberButtonCommand(object obj)
        {
            await this._calculatorLogic.ReceiveNumberCommand((string)obj);
        }

        private async Task OperatorButtonCommand(object obj)
        { 
            Enable = false;
            // Simulating caculate much time
            await Task.Delay(2000);
            await this._calculatorLogic.ReceiveOperatorCommand((string)obj);
        }

        private async Task OtherButtonCommand(object obj)
        {
            Enable = false;
            // Simulating caculate much time
            await Task.Delay(2000);
            await this._calculatorLogic.ReceiveOtherCommand((string)obj);
        }
    }
}
