using DIContainer;
using PI_Calculator.Interface;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace PI_Calculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
            NickiService serviceDescriptors = new NickiService();
            serviceDescriptors.Add<ICalculatorPresenter, CalculatorPresenter>(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient);
            serviceDescriptors.Add<Window, MainWindow>(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient);
            serviceDescriptors.Add<IPIService, PIService>(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient);
            IServiceProvider provider = serviceDescriptors.BuildServiceProvider();
            Window mainwindow = provider.GetService<Window>();
            
            mainwindow.Show(); 
        }
    }

}
