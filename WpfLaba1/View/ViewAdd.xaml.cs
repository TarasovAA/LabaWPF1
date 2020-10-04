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

namespace WpfLaba1.View
{
    /// <summary>
    /// Логика взаимодействия для ViewAdd.xaml
    /// </summary>
    public partial class ViewAdd : Window
    {
        public ViewAdd()
        {
            InitializeComponent();
        }

        public string HeroName;
        public string HeroHP;
        public string HeroEnergy;
        public string HeroSkill;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HeroName = Name.Text;
            HeroHP = HP.Text;
            HeroEnergy = Energy.Text;
            HeroSkill = Skills.Text;
            this.Close();
        }
    }
}
