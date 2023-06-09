﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorInstance.Base;
using System.Windows.Input;
using System.Windows.Controls;
using CalculatorInstance.Bases;

namespace CalculatorInstance.ViewModel
{
    internal class CalculatorViewModel : Base.ViewModelBase, ICalculatorViewModel
    {
        private AsyncCommand<object> _numberBtnCommand;
        private AsyncCommand<object> _operatorBtnCommand;
        private AsyncCommand<object> _otherBtnCommand;
        ICalculatorBusinessLogic _calculatorLogic;
        public CalculatorViewModel(ICalculatorBusinessLogic businessLogic)
        { 
            this._calculatorLogic = businessLogic;
            this._calculatorLogic.Value.Subscribe(x => { this.Value = x; });
            this._calculatorLogic.CurrentExpression.Subscribe(x => { this.CurrentExpression = x; });

            this._numberBtnCommand = new AsyncCommand<object>(this.NumberButtonCommand);
            this._operatorBtnCommand = new AsyncCommand<object>(this.OperatorButtonCommand);
            this._otherBtnCommand = new AsyncCommand<object>(this.OtherButtonCommand);
        }

        public ICommand NumberBtn => _numberBtnCommand;
        public ICommand OperatorBtn => _operatorBtnCommand;
        public ICommand OtherBtn => _otherBtnCommand;

        private string _currentExpression = "";
        public string CurrentExpression
        {
            get { return _currentExpression; } 
            set
            {
                _currentExpression = value;
                OnPropertyChanged("CurrentExpression");
            }
        }

        private string _value = "";
        public string Value
        {
            get { return _value; }
            set
            { 
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private async Task NumberButtonCommand(object obj)
        {
            await this._calculatorLogic.ReceiveNumberCommand((string)obj);
        }

        private async Task OperatorButtonCommand(object obj)
        { 
            await this._calculatorLogic.ReceiveOperatorCommand((string)obj);
        }

        private async Task OtherButtonCommand(object obj)
        { 
            await this._calculatorLogic.ReceiveOtherCommand((string)obj);
        }
    }
}
