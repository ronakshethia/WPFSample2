using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Services;
using WpfApp2.ViewModels;

namespace WpfApp2
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

        [ObservableProperty]
        private string pageTitle;

        [ObservableProperty]
        private BaseViewModel currentViewModel;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsOpen))]
        private BaseViewModel currentModalViewModel;

        private readonly ModalNavigationStore _modalNavigationStore;

        public bool IsOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            CurrentViewModel = _navigationStore.CurrentViewModel;
            CurrentModalViewModel = _modalNavigationStore.CurrentModalViewModel;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalNavigationStore.CurrentModalViewModelChanged += OnCurrentModalViewModelChnaged;
        }

        private void OnCurrentModalViewModelChnaged()
        {
            CurrentModalViewModel = _modalNavigationStore.CurrentModalViewModel;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModel = _navigationStore.CurrentViewModel;
        }

        public async virtual Task OnScreenLoaded()
        {
        }

        public async virtual Task OnScreenDismissed()
        {
        }
    }
}
