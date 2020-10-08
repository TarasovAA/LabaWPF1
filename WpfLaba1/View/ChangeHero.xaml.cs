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
using WpfLaba1.Models;

namespace WpfLaba1.View
{
    /// <summary>
    /// Логика взаимодействия для ChangeHero.xaml
    /// </summary>
    public partial class ChangeHero : Window
    {
        //public bool isClickButton = false;
        public ChangeHero()
        {
            InitializeComponent();
            //this.Name.Text = name;
            //this.Hp.Text = hp.ToString();
            //this.Energy.Text = energy.ToString();
            //this.Skills.Text = skills.ToString();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    isClickButton = true;
        //    this.Close();
        //}
    }
}
