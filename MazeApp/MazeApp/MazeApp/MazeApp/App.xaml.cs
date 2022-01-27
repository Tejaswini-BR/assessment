using MazeApp.Models;
using MazeApp.ViewModel;
using MazeApp.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace MazeApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMaze, Maze>(TypeLifetime.Singleton);
            container.RegisterType<IMazeViewModel, MazeViewModel>();
            container.RegisterType<IMazeNavigationHelper, MazeNavigationHelper>();

            var mazeViewModel = container.Resolve<IMazeViewModel>();
            var window = new MainWindow(mazeViewModel);
            window.Show();

        }
    }
}
