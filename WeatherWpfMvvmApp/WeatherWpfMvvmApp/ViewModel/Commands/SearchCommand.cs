using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherWpfMvvmApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherViewModel _vm { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public SearchCommand(WeatherViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object? parameter)
        {
            string? query = parameter as string;

            if (string.IsNullOrWhiteSpace(query))
                return false;

            return true;
        }

        public void Execute(object? parameter)
        {
            _vm.MakeQuery();
        }
    }
}
