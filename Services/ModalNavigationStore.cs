using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.ViewModels;

namespace WpfApp2.Services
{
    public class ModalNavigationStore
    {
        private BaseViewModel _currentModalViewModel;

        public BaseViewModel CurrentModalViewModel
        {
            get { return _currentModalViewModel; }
            set
            {

                _currentModalViewModel = value;
                OnCurrentModalViewModelChanged();
            }
        }

        public bool IsOpen => _currentModalViewModel != null;

        public event Action CurrentModalViewModelChanged;

        public void Close()
        {
            CurrentModalViewModel = null;
        }

        private void OnCurrentModalViewModelChanged()
        {
            CurrentModalViewModelChanged?.Invoke();
        }

    }
}