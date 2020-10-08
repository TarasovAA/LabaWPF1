using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLaba1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Data.Entity;
using WpfLaba1.View;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WpfLaba1.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        //Hero selectedHero;
        ISource source;
        public ISource Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
                onPropertyChanged("SelectedSource");
                onPropertyChanged("HeroesList");
            }
        }
        public List<ISource> DataSources { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ReadOnlyObservableCollection<Hero> HeroesList => source.HeroesList;

        List<Hero> insertHeroes;

        //Hero ChabgingHero;

        IList selectedHeroes;

        public IList SelectedHeroes
        {
            get
            {
                return selectedHeroes;
            }
            set
            {
                selectedHeroes = value;
                onPropertyChanged("SelectedHeroes");
            }
        }

        Hero selectedHero;
        public Hero SelectedHero
        {
            get
            {
                return selectedHero;
            }
            set
            {
                selectedHero = value;
                onPropertyChanged("SelectedHero");
            }
        }

        Hero newHero;
        public Hero NewHero
        {
            get
            {
                return newHero;
            }
            set
            {
                selectedHero = newHero;
                onPropertyChanged("newHero");
            }
        }

        public ApplicationViewModel()
        {
            DataSources = new List<ISource> { new BDmssql(), new JSONRecords() };
            insertHeroes = new List<Hero>();
            Source = DataSources[0];
            currentWindow = new ViewHeroes();
        }
        public Window currentWindow;

        RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ?? (updateCommand = new RelayCommand(
                    obj =>
                    {
                        foreach(ISource s in DataSources)
                        {
                            s.SaveChanges();
                        }
                    }));
            }
        }

      

        private void CloseCommanddb()
        {
            source.Dispose();
        }


        RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(
                    obj =>
                    {
                        List<Hero> heroesForDeleting = new List<Hero>();
                        foreach (var hero in (IList)obj)
                        {
                            heroesForDeleting.Add(hero as Hero);
                        }
                        foreach(Hero hero in heroesForDeleting)
                        {
                            source.Remove(hero);
                        }
                        onPropertyChanged("HeroesList");
                        
                    }, (obj) => source.Count>0));
            }
        }

        RelayCommand copyCommand;
        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand ?? (copyCommand = new RelayCommand(
                    obj =>
                    {
                        if(insertHeroes.Count>0)
                        {
                            insertHeroes.Clear();
                        }
                        foreach (var hero in (IList)obj)
                        {
                            insertHeroes.Add(hero as Hero);
                        }
                    }, (obj) => selectedHeroes != null && selectedHeroes.Count > 0));
            }
        }
        RelayCommand cutCommand;
        public RelayCommand CutCommand
        {
            get
            {
                return cutCommand ?? (cutCommand = new RelayCommand(
                    obj =>
                    {
                        if (insertHeroes.Count > 0)
                        {
                            insertHeroes.Clear();
                        }
                        foreach (var hero in (IList)obj)
                        {
                            insertHeroes.Add(hero as Hero);
                        }
                        foreach (Hero hero in insertHeroes)
                        {
                            source.Remove(hero);
                        }
                        onPropertyChanged("HeroesList");

                    }, (obj) => selectedHeroes!=null && selectedHeroes.Count > 0));
            }
        }

        RelayCommand pasteCommand;
        public RelayCommand PasteCommand
        {
            get
            {
                return pasteCommand ?? (pasteCommand = new RelayCommand(
                    obj => 
                    {
                        foreach(Hero hero in insertHeroes)
                        {
                            source.Add(hero);
                        }
                        insertHeroes.Clear();
                    }, (obj) => insertHeroes.Count > 0));
            }
        }

        RelayCommand beginChangeCommand;
        public RelayCommand BeginChangeCommand
        {
            get
            {
                return beginChangeCommand ?? (beginChangeCommand = new RelayCommand(
                    obj =>
                    {
                        currentWindow = new ChangeHero();
                        currentWindow.DataContext = this;
                        currentWindow.ShowDialog();

                    }, (obj) => selectedHero != null));
            }
        }


        RelayCommand finishChangeCommand;
        public RelayCommand FinishChangeCommand
        {
            get
            {
                return finishChangeCommand ?? (finishChangeCommand = new RelayCommand(
                    obj =>
                    {
                        currentWindow.Close();
                        //(obj as Hero)
                    }, obj => {
                        var results = new List<ValidationResult>();
                        var context = new ValidationContext(selectedHero);
                        if (!Validator.TryValidateObject(selectedHero, context, results, true))
                        {
                            return false;
                        }
                        return true;
                    }));
            }
        }
        RelayCommand startAddCommand;
        public RelayCommand StartAddCommand
        {
            get
            {
                return startAddCommand ?? (startAddCommand = new RelayCommand(
                    obj =>
                    {
                        newHero = new Hero();
                        currentWindow = new ViewAdd();
                        currentWindow.DataContext = this;
                        currentWindow.ShowDialog();
                    }));
            }
        }

        RelayCommand finishAddCommand;
        public RelayCommand FinishAddCommand
        {
            get
            {
                return finishAddCommand ?? (finishAddCommand = new RelayCommand(
                    obj =>
                    {
                        source.Add(newHero);
                        currentWindow.Close();
                    }, obj =>
                    {
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

        public void onPropertyChanged(string prop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
