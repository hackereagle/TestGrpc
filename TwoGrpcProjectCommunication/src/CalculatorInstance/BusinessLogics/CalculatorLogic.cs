using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorInstance.Base;
using CalculatorInstance;

namespace CalculatorInstance.BusinessLogics
{
    internal class CalculatorLogic
    {
        private ISubject<string> _curExpression; // display on upper textblock
        public IObservable<string> CurrentExpression => _curExpression.AsObservable();
        private ISubject<string> _curVal; // display on down textblock
        public IObservable<string> Value => _curVal.AsObservable();
        private ComputeServiceProxy.ComputeServiceProxy _computeServiceProxy;

        private Queue<string> _valQueue;
        public CalculatorLogic()
        { 
            this._valQueue = new Queue<string>();
            this._curVal = new Subject<string>();
            this._curExpression = new Subject<string>();

            this._computeServiceProxy = new ComputeServiceProxy.ComputeServiceProxy();
        }

        private string _valStash = "";
        private bool _isCalculated = false;
        public async Task ReceiveNumberCommand(string str)
        {
            await Task.Run(() =>
            {
                if (this._isCalculated)
                {
                    this._curExpression.OnNext("");
                    this._valStash = "";
                    this._curVal.OnNext(this._valStash);
                    this._isCalculated = false;
                }

                if (str == "+/-") { 
                    this._valStash = this._valStash[0] == '-' ? this._valStash.Substring(1) : "-" + this._valStash;
                }
                else
                    this._valStash = this._valStash + str;

                this._curVal.OnNext(this._valStash);
            });
        }

        private string Calculate(string num1, string opt, string num2)
        {
            return _computeServiceProxy.Calculate(num1, opt, num2);
        }

        public async Task ReceiveOperatorCommand(string str)
        {
            await Task.Run(() =>
            {
                if (str == "=" && this._valStash != "")
                {
                    if (this._valQueue.Count < 2)
                        return;

                    string num1 = this._valQueue.Dequeue();
                    string opt = this._valQueue.Dequeue();
                    string num2 = this._valStash;
                    this._isCalculated = true;

                    this._curExpression.OnNext($"{num1} {opt} {num2} = ");
                    this._valStash = Calculate(num1, opt, num2);
                    this._curVal.OnNext(this._valStash);
                }
                else
                {
                    if (this._valQueue.Count > 1)
                    { 
                        string num1 = this._valQueue.Dequeue();
                        string opt = this._valQueue.Dequeue();
                        string num2 = this._valStash;
                        this._valStash = Calculate(num1, opt, num2);
                        this._curVal.OnNext(this._valStash);
                    }

                    this._valQueue.Enqueue(this._valStash);
                    this._valQueue.Enqueue(str);
                    this._curExpression.OnNext($"{this._valStash} {str}");

                    this._valStash = "";
                    this._curVal.OnNext(this._valStash);
                }
            });
        }

        private string Calculate(string num1, string opt)
        {
            return _computeServiceProxy.Calculate(num1, opt);
        }

        public async Task ReceiveOtherCommand(string str)
        {
            await Task.Run(() =>
            {
                if (str == "C")
                {
                    this._valStash = "";
                    this._curVal.OnNext(this._valStash);
                    this._curExpression.OnNext(this._valStash);
                    this._valQueue.Clear();
                    this._isCalculated = false;
                }
                else if (str == "CE")
                {
                    this._valStash = "";
                    this._curVal.OnNext(this._valStash);
                }
                else if (str == "Del")
                {
                    if (this._isCalculated)
                    {
                        this._curExpression.OnNext("");
                    }
                    else if (this._valStash != "")
                    {
                        this._valStash = this._valStash.Remove(this._valStash.Length - 1);
                        this._curVal.OnNext(this._valStash);
                    }
                }
                else
                {
                    this._valStash = Calculate(this._valStash, str);
                    this._curVal.OnNext(this._valStash);
                }
            });
        }
    }
}
