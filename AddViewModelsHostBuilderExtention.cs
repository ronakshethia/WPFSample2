using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfApp2.Services;
using WpfApp2.ViewModels;
using WpfApp2.ViewModels.ViewModals;

namespace WpfApp2
{
    public static class AddViewModelsHostBuilderExtention
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<ModalNavigationStore>();
                services.AddTransient<DashboardViewModel>();
                services.AddTransient<ItemMasterViewModel>();
                services.AddTransient<SearchViewModel>();
                services.AddSingleton<ViewModelRouter>((s) =>
                new ViewModelRouter(new Dictionary<string, INavigationService>
                {
                    {
                        "Dashboard" ,
                        new NavigationService<DashboardViewModel>(
                            s.GetRequiredService<NavigationStore>(),
                            () => s.GetRequiredService<DashboardViewModel>())
                    },
                    {
                        "ItemMaster" ,
                        new NavigationService<ItemMasterViewModel>(
                            s.GetRequiredService<NavigationStore>(),
                            () => s.GetRequiredService<ItemMasterViewModel>())
                    },
                    {
                        "Search" ,
                        new ModalNavigationService<SearchViewModel>(
                            s.GetRequiredService<ModalNavigationStore>(),
                            () => s.GetRequiredService<SearchViewModel>())
                    },
                }));

            });

            return hostBuilder;
        }
    }
}
