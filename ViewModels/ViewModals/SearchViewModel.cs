using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Services;

namespace WpfApp2.ViewModels.ViewModals
{
    public partial class SearchViewModel : BaseViewModel, IOnReturnNavigationService
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public SearchViewModel(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public Action<object> OnReturn { get; set; }

        [RelayCommand]
        void Close()
        {
            OnReturn?.Invoke("Testing");
            _modalNavigationStore.Close();
        }
    }
}
