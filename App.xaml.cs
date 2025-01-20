using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp2.Services;
using WpfApp2.Services.HttpServices;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly IHost _host;

        public static ServiceProvider _container;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
               .AddViewModels()
               .ConfigureServices(services => {
                   services.AddSingleton<ApiService>(); // Add ApiService
                   services.AddSingleton<MainViewModel>();
                   services.AddSingleton<MainWindow>((s) => new MainWindow()
                   {
                       DataContext = s.GetRequiredService<MainViewModel>()
                   });


               }).Build();
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            var router = _host.Services.GetRequiredService<ViewModelRouter>();
            await router.AsyncNavigation("Dashboard", "Navigated");
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
