using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfLaba1.Models
{
    public class BDmssql: ISource //модел для sql данных
    {
        HeroesContext bd;
        public ReadOnlyObservableCollection<Hero> HeroesList { get; private set; }
        private ObservableCollection<Hero> heroList;

        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public int Count => HeroesList.Count;

        public override string ToString()
        {
            return "MsSQL Server";
        }


        public BDmssql()
        {
            bd = new HeroesContext();
            heroList = new ObservableCollection<Hero>(bd.Heroes.ToList());
            HeroesList = new ReadOnlyObservableCollection<Hero>(heroList);
        }


        public void Dispose()
        {
            bd.Dispose();
        }

        public bool Remove(Hero hero)
        {
            //bd.Heroes.Remove(hero);
            heroList.Remove(hero);
            //onPropertyChanged("ChangeHeroList");
            return true;
        }

        public void SaveChanges()
        {
            bd.Heroes.RemoveRange(bd.Heroes);
            bd.SaveChanges();
            bd.Heroes.AddRange(heroList.ToList<Hero>());    
            bd.SaveChanges();
        }

        public bool Add(Hero hero)
        {
            if(!heroList.Contains(hero))
            {

               // bd.Heroes.Add(hero);
                heroList.Add(hero);
                //onPropertyChanged("ChangeHeroList");
                return true;
            }
            return false;
        }

        public bool Change(Hero hero)
        {
            throw new NotImplementedException();
        }
    }
}
