using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorInstance.Base;

namespace CalculatorInstance.BusinessLogics
{
    internal class CalculatorLogic
    {
        private ISubject<string> _curExpression; // display on upper textblock
        public IObservable<string> CurrentExpression => _curExpression.AsObservable();
        private ISubject<string> _curVal; // display on down textblock
        public IObservable<string> Value => _curVal.AsObservable();

        private Queue<string> _valQueue;
        public CalculatorLogic()
        { 
            this._valQueue = new Queue<string>();
            this._curVal = new Subject<string>();
            this._curExpression = new Subject<string>();
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

        private T CalculateInstance<T>(T n1, T n2, string opt)
        {
            dynamic _n1 = n1;
            dynamic _n2 = n2;
            dynamic result = -9999999999999;
            if (opt == "+")
            {
                result = _n1 + _n2;
                return result;
            }
            else if (opt == "-")
            {
                result = _n1 - _n2;
                return result;
            }
            else if (opt == "*")
            {
                result = _n1 * _n2;
                return result;
            }
            else if (opt == "/")
            {
                result = _n1 / _n2;
                return result;
            }
            else
            {
                throw new ArgumentException("Undefined operator!");
            }
        }

        private string Calculate(string num1, string opt, string num2)
        { 
            bool byDouble = num1.IndexOf(".") != -1 || num2.IndexOf(".") != -1;

            if (byDouble)
            { 
                double n1 = double.Parse(num1);
                double n2 = double.Parse(num2);
                return $"{CalculateInstance<double>(n1, n2, opt)}";
            }
            else
            { 
                int n1 = int.Parse(num1);
                int n2 = int.Parse(num2);
                return $"{CalculateInstance<int>(n1, n2, opt)}";
            }
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

        private double CalculateInstance<T>(T n1, string opt)
        {
            double _n1 = Convert.ToDouble(n1);
            double result = -9999999999999.0;
            if (opt == "%")
            {
                result = Convert.ToDouble(_n1) / 100.0;
                return result;
            }
            else if (opt == "1/x")
            {
                result = 1.0 / _n1;
                return result;
            }
            else if (opt == "x^2")
            {
                result = Math.Pow(_n1, 2.0);
                return result;
            }
            else if (opt == "x^(1/2)")
            {
                result = Math.Sqrt(_n1);
                return result;
            }
            else
            {
                throw new ArgumentException("Undefined operator!");
            }
        }

        private string Calculate(string num1, string opt)
        { 
            bool byDouble = num1.IndexOf(".") != -1;

            if (byDouble)
            { 
                double n1 = double.Parse(num1);
                return $"{CalculateInstance<double>(n1, opt)}";
            }
            else
            { 
                int n1 = int.Parse(num1);
                return $"{CalculateInstance<int>(n1, opt)}";
            }
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
