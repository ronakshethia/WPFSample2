using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp2.Utilities
{
    public abstract partial class CommandBase : ObservableObject, ICommand
    {
        #region 

        [ObservableProperty]
        private string label;

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (SetProperty(ref _isEnabled, value))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return IsEnabled;
        }

        public virtual void Execute(object parameter)
        {
        }
    }
}
