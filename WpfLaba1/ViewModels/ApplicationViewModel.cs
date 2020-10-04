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
            }
        }
        public List<ISource> DataSources { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ReadOnlyObservableCollection<Hero> HeroesList => source.HeroesList;

        List<Hero> insertHeroes;

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

        //public Hero SelectedHero
        //{
        //    get
        //    {
        //        int[] a = { };
        //        int e =a.Length;
        //        return selectedHero;
        //    }
        //    set
        //    {
        //        selectedHero = value;
        //        onPropertyChanged("SelectedHero");
        //    }
        //}

        public ApplicationViewModel()
        {
            DataSources = new List<ISource> { new BDmssql(), new TextRecords() };
            insertHeroes = new List<Hero>();
            Source = DataSources[0];
        }

        RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ?? (updateCommand = new RelayCommand(
                    obj =>
                    {
                        source.SaveChanges();
                    }));
            }
        }

       RelayCommand addCommand;
       public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(
                    obj =>
                    {
                        ViewAdd addWindow;
                        bool nextHero = false;
                        Hero hero = null;
                        do
                        {
                            addWindow = new ViewAdd();
                            addWindow.ShowDialog();
                            try
                            {
                                hero = new Hero() { Name = addWindow.HeroName, Hp = Int32.Parse(addWindow.HeroHP), Energy = Int32.Parse(addWindow.HeroEnergy), Skills = addWindow.HeroSkill };
                            }
                            catch(Exception Error)
                            {
                                MessageBox.Show(Error.ToString());
                                continue;
                            }
                            var results = new List<ValidationResult>();
                            var context = new ValidationContext(hero);
                            if (!Validator.TryValidateObject(hero, context, results, true))
                            {
                                string Error = "";
                                foreach (var error in results)
                                {
                                    Error += error.ToString();
                                }
                                MessageBox.Show(Error);
                                continue;
                            }
                            nextHero = true;
                        }
                        while (!nextHero);
                        source.Add(hero);
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

        public void onPropertyChanged(string prop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
