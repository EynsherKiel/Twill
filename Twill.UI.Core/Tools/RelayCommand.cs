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
}
