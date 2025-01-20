using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Reactive;
using System.Reactive.Linq;
using WpfApp2.Services;
using WpfApp2.Services.HttpServices;

namespace WpfApp2.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel, IParameterNavigationService, IAsyncNavigationService
    {
        [ObservableProperty]
        private string pageTitle;

        private readonly ViewModelRouter _viewModelRouter;
        private readonly ApiService _apiService;

        public DashboardViewModel(ViewModelRouter viewModelRouter, ApiService apiService)
        {
            _viewModelRouter = viewModelRouter;
            _apiService = apiService;
        }

        public async Task InitialzeAsync()
        {
            await Task.Delay(1000);  // Simulate async work.
        }

        public void ParameterInitialize(params object[] parameters)
        {
            PageTitle = parameters[0].ToString();
            // Use parameters as needed.
        }

        [RelayCommand]
        public async Task GoToItemMaster()
        {
            _viewModelRouter.NavigateTo("Search", ["Name", new Action<object>(OnActionSelected)]);
            //await _viewModelRouter.AsyncNavigation("ItemMaster");
        }

        private void OnActionSelected(object obj)
        {
        }

        private IObservable<Unit> FetchData()
        {
            return _apiService.CallApi<string[]>("https://dummyjson.com/products")
           .Do(result =>
           {
              
           })
           .Select(_ => Unit.Default)
           .Catch<Unit, Exception>(ex =>
           {
               // Handle errors gracefully
               Console.WriteLine($"Error: {ex.Message}");
               return Observable.Return(Unit.Default);
           });
        }
    }
}
