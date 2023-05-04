using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CalculatorInstance
{
    class CalculatorInstanceNavigateService
    {
        private static readonly Lazy<CalculatorInstanceNavigateService> _instance = new Lazy<CalculatorInstanceNavigateService>(() => new CalculatorInstanceNavigateService());
        public static CalculatorInstanceNavigateService Instance => _instance.Value;

        ServiceProvider _provider;

        private CalculatorInstanceNavigateService()
        { 
            var sc = new ServiceCollection();
            sc.AddSingleton<Bases.ICalculatorBusinessLogic, BusinessLogics.CalculatorLogic>();
            sc.AddSingleton<Bases.ICalculatorViewModel, ViewModel.CalculatorViewModel>(sp =>
            {
                return new ViewModel.CalculatorViewModel(sp.GetRequiredService<Bases.ICalculatorBusinessLogic>());
            });

            _provider = sc.BuildServiceProvider();
        }

        public object GetService<T>()
        {
            object service = null;
            service = _provider.GetService<T>()!;
            if (service == null)
                throw new ArgumentException($"There is not {nameof(service)} type.");
            return service;
        }
    }
}
