using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfLaba1.ViewModels;
using WpfLaba1.View;

namespace WpfLaba1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        ApplicationViewModel av = new ApplicationViewModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new ViewHeroes() { DataContext = av }.Show();
            av.onPropertyChanged("ClosingWindow");
        }

    }
}
