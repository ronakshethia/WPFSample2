﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.ViewModels;

namespace WpfApp2.Services
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(params object[] parameters)
        {
            var viewModel = _createViewModel();

            if (parameters.Length > 0 && viewModel is IParameterNavigationService paramViewModel)
            {
                paramViewModel.ParameterInitialize(parameters);
            }

            _navigationStore.CurrentViewModel = viewModel;
        }

        public async Task AsyncNavigation(params object[] parameters)
        {
            var viewModel = _createViewModel();

            if (parameters.Length > 0 && viewModel is IParameterNavigationService paramViewModel)
            {
                paramViewModel.ParameterInitialize(parameters);
            }

            if (viewModel is IAsyncNavigationService asyncViewModel)
            {
                await asyncViewModel.InitialzeAsync();
            }

            _navigationStore.CurrentViewModel = viewModel;
        }
    }
}
