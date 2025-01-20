using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.ViewModels;

namespace WpfApp2.Services
{
    public class ModalNavigationService<TViewModel> : INavigationService where TViewModel : BaseViewModel
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public ModalNavigationService(ModalNavigationStore modalNavigationStore, Func<TViewModel> createViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            _createViewModel = createViewModel;
        }

        public async Task AsyncNavigation(params object[] parameters)
        {
            var viewModel = _createViewModel();

            if (parameters.Length > 0 && parameters != null)
            {
                if (viewModel is IParameterNavigationService paramViewModel)
                {
                    paramViewModel.ParameterInitialize(parameters);
                }
            }

            if (viewModel is IAsyncNavigationService asyncViewModel)
            {
                await asyncViewModel.InitialzeAsync();
            }

            _modalNavigationStore.CurrentModalViewModel = viewModel;
        }

        public void Navigate(params object[] parameters)
        {
            var viewModel = _createViewModel();

            if (parameters.Length > 0 && parameters != null)
            {
                if (viewModel is IParameterNavigationService paramViewModel)
                {
                    paramViewModel.ParameterInitialize();
                }

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (viewModel is IOnReturnNavigationService onReturnNavigationService &&
                        parameters[i] is Action<object> returnCallback)
                    {
                        onReturnNavigationService.OnReturn = returnCallback;
                    }
                }
            }

            _modalNavigationStore.CurrentModalViewModel = viewModel;
        }

        public void Close()
        {
            _modalNavigationStore?.Close();
        }
    }
}
