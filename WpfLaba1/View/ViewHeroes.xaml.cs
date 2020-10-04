using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfLaba1.ViewModels;
using WpfLaba1.Models;


namespace WpfLaba1.View
{
    /// <summary>
    /// Логика взаимодействия для ViewHeroes.xaml
    /// </summary>
    public partial class ViewHeroes : Window
    {

        public ViewHeroes()
        {
            InitializeComponent();
            ApplicationViewModel av = new ApplicationViewModel();
            DataContext = av;
            this.Closing += (s, e) => { av.onPropertyChanged("ClosingWindow"); };
        }
       
    }
}
