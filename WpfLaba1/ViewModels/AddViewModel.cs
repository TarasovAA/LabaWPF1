using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfLaba1.Models;

namespace WpfLaba1.ViewModels
{
    public class AddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        Window window; 
        Hero newHero;
        public Hero NewHero
        {
            get { return newHero; }
            set { newHero = value; }
        }

        public AddViewModel(Window window)
        {
            newHero = new Hero();
            this.window = window;
            window.DataContext = this;
            window.ShowDialog();
        }

        RelayCommand finishAddCommand;
        public RelayCommand FinishAddCommand
        {
            get
            {
                return finishAddCommand ?? (finishAddCommand = new RelayCommand(
                    obj =>
                    {
                        window.Close();
                    }, obj =>
                    {
                        //TODO: дореализовать валидацию до конца(средняя важность)
                        var results = new List<ValidationResult>();
                        var context = new ValidationContext(newHero);
                        if (!Validator.TryValidateObject(newHero, context, results, true))
                        {
                            return false;
                        }
                        return true;
                    }));
            }
        }
    }
}
