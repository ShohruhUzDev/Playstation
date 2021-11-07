using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Playstation.WPF.Context;
using Playstation.WPF.Interfaces;
using Playstation.WPF.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Playstation.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly IServiceProvider serviceProvider;
        public App()
        {
            //IServiceCollection services = new ServiceCollection();
            //ConfigureServices(services);
            //serviceProvider = services.BuildServiceProvider();

           
        }

        //private void ConfigureServices(IServiceCollection services)
        //{
        //    //services.AddSingleton<MainWindow>();
        //    services.AddSingleton<IDeviceService, DeviceService>();
        //    services.AddSingleton<ITarrifService, TarrifService>();
        //    services.AddSingleton<IOrderService, OrderService>();


        //}
        //private void OnStartup(object sender, StartupEventArgs e)
        //{
        //    var mainWindow = serviceProvider.GetService<MainWindow>();
        //     mainWindow.Show();
        //}
}
}
