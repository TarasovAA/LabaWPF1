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
    public class ChangeHeroModelViewModel: INotifyPropertyChanged
    {
        Window window;
        Hero changingHero;
        Hero currentHero;  //на данный момент рудимент, но может пригодиться

        public Hero ChangingHero
        {
            get
            {
                return changingHero;
            }
            set
            {
                changingHero = value;
                onPropertyChanged("ChangingHero");
            }
        }
        public ChangeHeroModelViewModel(Window window, Hero currentHero)
        {
            ChangingHero = currentHero;
            //ChangingHero = new Hero() { Name = currentHero.Name, Hp=currentHero.Hp, Energy = currentHero.Energy, Skills = currentHero.Skills, Id = currentHero.Id};
            this.window = window;
            window.DataContext = this;
            window.ShowDialog();
        }

        RelayCommand finishChangeCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string prop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public RelayCommand FinishChangeCommand
        {
            get
            {
                return finishChangeCommand ?? (finishChangeCommand = new RelayCommand(
                    obj =>
                    {
                        //currentHero.Name = ChangingHero.Name;
                        //currentHero.Hp = ChangingHero.Hp;
                        //currentHero.Energy = ChangingHero.Energy;
                        //currentHero.Skills = ChangingHero.Skills;
                        window.Close();
                        //(obj as Hero)
                    }, obj => {
                    //TODO: дореализовать валидацию до конца(средняя важность)
                        var results = new List<ValidationResult>();  //частичная валидация данных 
                        var context = new ValidationContext(changingHero);
                        if (!Validator.TryValidateObject(changingHero, context, results, true))
                        {
                            return false;
                        }
                        return true;
                    }));
            }
        }
    }
}
