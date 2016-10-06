using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Twill.UI.Core.Tools
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action handler)
        {
            _handler = handler;
        }

        private readonly Action _handler;


        public bool CanExecute(object parameter) => true;

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter) => _handler();
    }

    public class RelayCommand<T> : ICommand where T : class
    {
        public RelayCommand(Action<T> handler)
        {
            _handler = handler;
        }

        private readonly Action<T> _handler;


        public bool CanExecute(object parameter) => true;

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter) => _handler(parameter as T);
    }
}
